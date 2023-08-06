import React, { useEffect, useState } from 'react'
import {
    useLocation
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import PhotoDetail from '../../elements/PhotoDetail';
import VideoDetail from '../../elements/VideoDetail';
import PdfDetail from '../../elements/PdfDetail';
import { getChoureyOneDetail } from '../../../actions/choureyAction';
import { manipulateFileData } from '../../../util/util';

const UserChoureyOneDetail = () => {

    const dispatch = useDispatch();
    const location = useLocation();
    const params = new URLSearchParams(location.search);
    const id = params.get('id');
    const [title, setTitle] = useState("");
    const [range, setRange] = useState(0);
    const [editorHtml, setEditorHtml] = useState('');
    const choureyDetails = useSelector((state) => state.chourey);
    const { choureyOneDetail } = choureyDetails
    var siteId = localStorage.getItem('siteId');

    const [data, setData] = useState([])
    const [videoData, setVideoData] = useState([])
    const [pdfData, setPdfData] = useState([])

    var userInfo = localStorage.getItem('userInfo');
    
    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getChoureyOneDetail(id, siteId))
        }
    }, [siteId])
    
    useEffect(() => {
        if (choureyOneDetail.choureyOneInfo) {
            if (choureyOneDetail.choureyOneInfo.imageList.length > 0) {
                manipulateFileData(choureyOneDetail.choureyOneInfo.imageList, 'image/png', userInfo, setData, setVideoData, setPdfData)
            }

            if (choureyOneDetail.choureyOneInfo.videoList.length > 0) {
                manipulateFileData(choureyOneDetail.choureyOneInfo.videoList, 'video/mp4', userInfo, setData, setVideoData, setPdfData)
            }

            if (choureyOneDetail.choureyOneInfo.fileList.length > 0) {
                manipulateFileData(choureyOneDetail.choureyOneInfo.fileList, 'application/pdf', userInfo, setData, setVideoData, setPdfData)
            }

            setTitle(choureyOneDetail.choureyOneInfo.title)
            setRange(choureyOneDetail.choureyOneInfo.browseRange)
            setEditorHtml(choureyOneDetail.choureyOneInfo.description)
        }
    },[choureyOneDetail])

    return (
        <>
            <div
                class="flex flex-col overflow-hidden bg-white rounded-md shadow-lg max md:flex-row md:flex-1"
            >
                <div class="flex-1 p-5 bg-white">
                    <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-2xl font-semibold">
                            {title}
                        </h3>
                    </div>
                    <div className="flex flex-col items-start p-2">
                        <h4>GPS Range(m): {range}</h4>
                        <h4 dangerouslySetInnerHTML={{ __html: editorHtml }}/>
                    </div>
                </div>
            </div>
            <PhotoDetail data={data} user={true} type={'choureyOne'} id={choureyOneDetail?.choureyOneInfo?.choureyOneId}/>
            <VideoDetail data={videoData} user={true} type={'choureyOne'} id={choureyOneDetail?.choureyOneInfo?.choureyOneId}/>
            <PdfDetail data={pdfData} user={true} type={'choureyOne'} id={choureyOneDetail?.choureyOneInfo?.choureyOneId}/>
        </>
    )
}

export default UserChoureyOneDetail