import React, { useEffect, useState } from 'react'
import {
    useLocation
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import { getChoureyTwoDetail } from '../../../../actions/choureyAction';
import PhotoDetail from '../../../elements/PhotoDetail';
import PdfDetail from '../../../elements/PdfDetail';
import VideoDetail from '../../../elements/VideoDetail';
import { manipulateFileData } from '../../../../util/util';

const ChoureyTwoDetail = () => {

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
    const { choureyTwoDetail } = choureyDetails

    const [data, setData] = useState([])
    const [videoData, setVideoData] = useState([])
    const [pdfData, setPdfData] = useState([])

    var userInfo = localStorage.getItem('userInfo');

    useEffect(() => {
        if (userSiteList.siteList != undefined) {
            dispatch(getChoureyTwoDetail(id, userSiteList?.siteList[0].id))
        }
    }, [userSiteList.siteList])

    useEffect(() => {
        if (choureyTwoDetail.choureyTwoInfo) {
            if (choureyTwoDetail.choureyTwoInfo.imageList.length > 0) {
                manipulateFileData(choureyTwoDetail.choureyTwoInfo.imageList, 'image/png', userInfo, setData, setVideoData, setPdfData)
            }

            if (choureyTwoDetail.choureyTwoInfo.videoList.length > 0) {
                manipulateFileData(choureyTwoDetail.choureyTwoInfo.videoList, 'video/mp4', userInfo, setData, setVideoData, setPdfData)
            }

            if (choureyTwoDetail.choureyTwoInfo.fileList.length > 0) {
                manipulateFileData(choureyTwoDetail.choureyTwoInfo.fileList, 'application/pdf', userInfo, setData, setVideoData, setPdfData)
            }

            setTitle(choureyTwoDetail.choureyTwoInfo.title)
            setRange(choureyTwoDetail.choureyTwoInfo.browseRange)
            setEditorHtml(choureyTwoDetail.choureyTwoInfo.description)
        }
    },[choureyTwoDetail])

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
            <PhotoDetail data={data} user={false} type={'choureyTwo'} id={choureyTwoDetail?.choureyTwoInfo?.choureyTwoId}/>
            <VideoDetail data={videoData} user={false} type={'choureyTwo'} id={choureyTwoDetail?.choureyTwoInfo?.choureyTwoId}/>
            <PdfDetail data={pdfData} user={false} type={'choureyTwo'} id={choureyTwoDetail?.choureyTwoInfo?.choureyTwoId}/>
        </>
    )
}

export default ChoureyTwoDetail