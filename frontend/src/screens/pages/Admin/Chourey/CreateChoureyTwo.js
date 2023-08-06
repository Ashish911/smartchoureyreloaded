import React, { useCallback, useEffect, useState } from 'react'
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { useTranslation } from 'react-i18next'
import PdfUpload from '../../../elements/PdfUpload';
import PhotoUpload from '../../../elements/PhotoUpload';
import {
    useNavigate,
    useLocation
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import { createTwoChourey, getChoureyTwoDetail, updateChoureyTwo } from '../../../../actions/choureyAction';
import VideoUpload from '../../../elements/VideoUpload';
import { toast } from 'react-toastify';
import PhotoEditDisplay from '../../../elements/PhotoEditDisplay';
import VideoEditDisplay from '../../../elements/VideoEditDisplay';
import PdfEditDisplay from '../../../elements/PdfEditDisplay';
import Loader from '../../../elements/Loader';
import { blobConversion, manipulateFileData } from '../../../../util/util';
import ConfirmationDialog from '../../../elements/ConfirmationDialog';
import { DELETE_MEDIA_FILE_RESET } from '../../../../contsants/fileConstants';

const CreateChoureyTwo = ({ isEdit }) => {

    let history = useNavigate();
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
    const [videoFiles, setVideoFiles] = useState([]);

    const siteDetails = useSelector((state) => state.site);
    const { userSiteList } = siteDetails
    const choureyDetails = useSelector((state) => state.chourey);
    const { choureyTwoDetail, createChoureyTwo, choureyTwoUpdate } = choureyDetails
    const fileDetails = useSelector((state) => state.file);
    const [isLoading, setIsLoading] = useState(false)

    const [photoData, setPhotoData] = useState('')
    const [videoData, setVideoData] = useState('')
    const [pdfData, setPdfData] = useState('')
    const [deleteModal, setDeleteModal] = useState(false)
    const [selectedData, setSelectedData] = useState({})

    var siteId = localStorage.getItem('siteId');

    var userInfo = localStorage.getItem('userInfo');

    const {t} = useTranslation()

    const handleChange = (html) => {
        setEditorHtml(html);
    }

    const submitHandler = (e) => {
        e.preventDefault();
        let params = {
            description: editorHtml,
            title: title,
            isActive: enabled,
            siteId: siteId,
            gpsRange: range,
            files: files,
            videos: videoFiles,
            uploads: pdfFiles,
            token: JSON.parse(userInfo).token
        }

        if (isEdit) {
            params.id = id
            params.photoId = choureyTwoDetail.choureyTwoInfo.photoId == null ? '' : choureyTwoDetail.choureyTwoInfo.photoId
            params.galleryId = choureyTwoDetail.choureyTwoInfo.galleryId
            dispatch(updateChoureyTwo(params))
        } else {
            dispatch(createTwoChourey(params))
        }
    }

    const toolbarOptions = {
        container : [
            [{ font: ['Arial', 'Helvetica', 'Courier', 'Comic Sans MS'] }],
            [{ header: [1, 2, 3, 4, 5, 6, false] }],
            ["bold", "italic", "underline", "strike"],
            [{ color: [] }, { background: [] }],
            [{ script:  "sub" }, { script:  "super" }],
            ["blockquote", "code-block"],
            [{ list:  "ordered" }, { list:  "bullet" }],
            [{ indent:  "-1" }, { indent:  "+1" }, { align: [] }],
            ["link", "image", "video"],
            ["clean"],
        ]
    };
    
    useEffect(() => {
        if (isEdit) {
            if (userSiteList.siteList != undefined) {
                dispatch(getChoureyTwoDetail(id, userSiteList?.siteList[0].id))
            }
        } 
    }, [userSiteList.siteList])

    useEffect(() => {
        if (isEdit) {
            if (choureyTwoDetail.choureyTwoInfo) {
                if (choureyTwoDetail.choureyTwoInfo.imageList.length > 0) {
                    manipulateFileData(choureyTwoDetail.choureyTwoInfo.imageList, 'image/png', userInfo, setPhotoData, setVideoData, setPdfData)
                }

                if (choureyTwoDetail.choureyTwoInfo.videoList.length > 0) {
                    manipulateFileData(choureyTwoDetail.choureyTwoInfo.videoList, 'video/mp4', userInfo, setPhotoData, setVideoData, setPdfData)
                }

                if (choureyTwoDetail.choureyTwoInfo.fileList.length > 0) {
                    manipulateFileData(choureyTwoDetail.choureyTwoInfo.fileList, 'application/pdf', userInfo, setPhotoData, setVideoData, setPdfData)
                }

                setTitle(choureyTwoDetail.choureyTwoInfo.title)
                setRange(choureyTwoDetail.choureyTwoInfo.browseRange)
                setEditorHtml(choureyTwoDetail.choureyTwoInfo.description)
                setEnabled(choureyTwoDetail.choureyTwoInfo.isActive)
            }
        }
    },[choureyTwoDetail])

    useEffect(() => {
        if (isEdit && choureyTwoUpdate) {
            setIsLoading(choureyTwoUpdate.loading)
            if (choureyTwoUpdate.error) {
                toast.error(choureyTwoUpdate.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }   else if (choureyTwoUpdate.success == true) {
                history('/dashboard/choureyTwo')
            }
        } else if (createChoureyTwo) {
            setIsLoading(createChoureyTwo.loading)
            if (createChoureyTwo.error) {
                toast.error(createChoureyTwo.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }   else if (createChoureyTwo.success == true) {
                history('/dashboard/choureyTwo')
            }
        }
    },[createChoureyTwo, choureyTwoUpdate])

    useEffect(() => {
        if (isEdit && fileDetails) {
            setIsLoading(fileDetails.loading)
            if (fileDetails.error) {
                toast.error(fileDetails.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
                dispatch({ type: DELETE_MEDIA_FILE_RESET })
            }   else if (fileDetails.success == true) {
                toast.success("Media Successfully Deleted", {
                    position: toast.POSITION.TOP_RIGHT
                })
                if (fileDetails.type == 1) {
                    deleteFromUI(photoData, fileDetails.uniqueId, setPhotoData)
                } else if (fileDetails.type == 2) {
                    deleteFromUI(videoData, fileDetails.uniqueId, setVideoData)
                } else {
                    deleteFromUI(pdfData, fileDetails.uniqueId, setPdfData)
                }
                dispatch({ type: DELETE_MEDIA_FILE_RESET })
            }
        }
    },[fileDetails])

    function deleteFunction() {
        if (deleteModal == true) {
            setSelectedData({})
            setDeleteModal(false)
        }
    }

    function deleteFromUI(data, uniqueId, setData) {
        let datas = []
        let index = data.findIndex(obj => obj.uniqueId === uniqueId)
            data.splice(index, 1)
            let no = 1
            data.map((element) => {
                let obj = {
                    id: no,
                    url: element.url,
                    uniqueId: element.uniqueId
                }
                no++
                datas.push(obj)
            })
        setData(datas)
    }

    return (
        <>
            {isLoading && <Loader />}
            <div
                class="flex flex-col overflow-hidden bg-white rounded-md shadow-lg max md:flex-row md:flex-1"
            >
                <div class="flex-1 p-5 bg-white">
                <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                    <h3 className="text-2xl font-semibold">
                    {t("Chourey Two")}
                    </h3>
                </div>
                <form action="#" class="flex flex-col space-y-5 mt-2">
                    <div class="flex flex-col md:flex-row space-y-1 mb-10">
                        <div className='flex flex-1 flex-col p-1'>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 w-2/3 flex-col p-1'>
                                    <label for="title" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Title')}:*</label>
                                    <input
                                        type="text"
                                        id="title"
                                        value={title}
                                        onChange={(e) => setTitle(e.target.value)}
                                        autoFocus
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                                <div className='flex flex-col p-1 w-1/3'>
                                    <label for="text" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Active')}</label>
                                        <label class="inline-flex relative items-center mr-5 cursor-pointer">
                                        <input
                                            type="checkbox"
                                            className="sr-only peer"
                                            checked={enabled}
                                            readOnly
                                        />
                                        <div
                                            onClick={() => {
                                                setEnabled(!enabled);
                                            }}
                                            className="w-11 h-6 bg-gray-200 rounded-full peer  peer-focus:ring-green-300  peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-0.5 after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-600"
                                        ></div>
                                        <span className="ml-2 text-sm font-medium text-gray-900">
                                            ON
                                        </span>
                                    </label>
                                </div>
                            </div>
                            <div class="flex flex-row space-y-1 items-end w-1/3">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="GPS" class="text-sm font-semibold text-gray-500 flex flex-start">{t('GPS Range(m)')}:*</label>
                                    <input
                                        type="text"
                                        id="GPS"
                                        value={range}
                                        onChange={(e) => setRange(e.target.value)}
                                        autoFocus
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                        <ReactQuill
                            style={{ height: '300px', width: '100%' }}
                            value={editorHtml}
                            onChange={handleChange}
                            modules={{ toolbar: toolbarOptions }}
                        />
                        </div>
                    </div>
                    
                    <PhotoUpload setFiles={setFiles} files={files} />
                    {isEdit && <PhotoEditDisplay data={photoData} type={"ChoureyTwo"} 
                    categoryId={choureyTwoDetail?.choureyTwoInfo?.choureyTwoId} 
                    setDeleteModal={setDeleteModal} setData={setSelectedData}/> }

                    <VideoUpload videoFiles={videoFiles} setVideoFiles={setVideoFiles} />
                    {isEdit && <VideoEditDisplay data={videoData} type={"ChoureyTwo"} 
                    categoryId={choureyTwoDetail?.choureyTwoInfo?.choureyTwoId} 
                    setDeleteModal={setDeleteModal} setData={setSelectedData}/> }

                    <PdfUpload pdfFiles={pdfFiles} setPdfFiles={setPdfFiles} />
                    {isEdit && <PdfEditDisplay data={pdfData} type={"ChoureyTwo"} 
                    categoryId={choureyTwoDetail?.choureyTwoInfo?.choureyTwoId} 
                    setDeleteModal={setDeleteModal} setData={setSelectedData}/> }
                    
                    <div>
                        <button
                            onClick={submitHandler}
                            type="submit"
                            class="w-full px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                        >
                            {isEdit ?
                                t('Edit')
                            :
                                t('Create')
                            }
                        </button>
                    </div>
                </form>
                </div>
            </div>
            {deleteModal ? (
                <>
                    <ConfirmationDialog deleteFunction={deleteFunction} text={'Media File'} params={selectedData} type={'MEDIADELETE'} />
                </>
            ) : null }
        </>
    )
}

export default CreateChoureyTwo