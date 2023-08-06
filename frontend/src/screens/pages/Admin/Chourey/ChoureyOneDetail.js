import React, { useEffect, useState } from 'react'
import {
    useLocation
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import { getChoureyOneDetail } from '../../../../actions/choureyAction';
import PhotoDetail from '../../../elements/PhotoDetail';
import PdfDetail from '../../../elements/PdfDetail';
import VideoDetail from '../../../elements/VideoDetail';
import { manipulateFileData } from '../../../../util/util';

const ChoureyOneDetail = () => {

    const dispatch = useDispatch();
    const location = useLocation();
    const params = new URLSearchParams(location.search);
    const id = params.get('id');
    const [title, setTitle] = useState("");
    const [range, setRange] = useState(0);
    const [editorHtml, setEditorHtml] = useState('');
    const siteDetails = useSelector((state) => state.site);
    const { userSiteList } = siteDetails
    const choureyDetails = useSelector((state) => state.chourey);
    const { choureyOneDetail } = choureyDetails

    const [data, setData] = useState([])
    const [videoData, setVideoData] = useState([])
    const [pdfData, setPdfData] = useState([])

    var userInfo = localStorage.getItem('userInfo');

    useEffect(() => {
        if (userSiteList.siteList != undefined) {
            dispatch(getChoureyOneDetail(id, userSiteList?.siteList[0].id))
        }
    }, [userSiteList.siteList])

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
            <PhotoDetail data={data} user={false} type={'choureyOne'} id={choureyOneDetail?.choureyOneInfo?.choureyOneId}/>
            <VideoDetail data={videoData} user={false} type={'choureyOne'} id={choureyOneDetail?.choureyOneInfo?.choureyOneId}/>
            <PdfDetail data={pdfData} user={false} type={'choureyOne'} id={choureyOneDetail?.choureyOneInfo?.choureyOneId}/>
        </>
    )
}

export default ChoureyOneDetail