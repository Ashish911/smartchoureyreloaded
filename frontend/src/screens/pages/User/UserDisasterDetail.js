import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import {
    useLocation
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import PhotoDetail from '../../elements/PhotoDetail';
import PdfDetail from '../../elements/PdfDetail';
import { getDisasterDetail } from '../../../actions/disasterAction';
import { manipulateFileData } from '../../../util/util';

const UserDisasterDetail = () => {

    const dispatch = useDispatch();
    const location = useLocation();
    const params = new URLSearchParams(location.search);
    const id = params.get('id');
    const [title, setTitle] = useState("");
    const [range, setRange] = useState(0);
    const [editorHtml, setEditorHtml] = useState('');
    const [enabled, setEnabled] = useState(false)
    const [files, setFiles] = useState([]);
    const [pdfFiles, setPdfFiles] = useState([]);
    const disasterDetails = useSelector((state) => state.disaster);
    const { disasterDetail } = disasterDetails
    var siteId = localStorage.getItem('siteId');

    const {t} = useTranslation()

    const [data, setData] = useState([])
    const [pdfData, setPdfData] = useState([])
    var userInfo = localStorage.getItem('userInfo');

    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getDisasterDetail(id, siteId))
        }
    }, [siteId])

    useEffect(() => {
        if (disasterDetail.disasterInfo) {
            if (disasterDetail.disasterInfo.imageList.length > 0) {
                manipulateFileData(disasterDetail.disasterInfo.imageList, 'image/png', userInfo, setData, null, setPdfData)
            }

            if (disasterDetail.disasterInfo.fileList.length > 0) {
                manipulateFileData(disasterDetail.disasterInfo.fileList, 'application/pdf', userInfo, setData, null, setPdfData)
            }

            setTitle(disasterDetail.disasterInfo.title)
            setRange(disasterDetail.disasterInfo.browseRange)
            setEditorHtml(disasterDetail.disasterInfo.description)
            setEnabled(disasterDetail.disasterInfo.isActive)
        }
    },[disasterDetail])

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
            <PhotoDetail data={data} user={true} type={'disaster'} id={disasterDetail?.disasterInfo?.disasterId}/>
            <PdfDetail data={pdfData} user={true} type={'disaster'} id={disasterDetail?.disasterInfo?.disasterId}/>
        </>
    )
}

export default UserDisasterDetail