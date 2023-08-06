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
import { disasterCreate, getDisasterDetail, updateDisaster } from '../../../../actions/disasterAction';
import { useDispatch, useSelector } from 'react-redux';
import Loader from '../../../elements/Loader';
import { toast } from 'react-toastify';
import PhotoEditDisplay from '../../../elements/PhotoEditDisplay';
import PdfEditDisplay from '../../../elements/PdfEditDisplay';
import { manipulateFileData } from '../../../../util/util';
import { DELETE_MEDIA_FILE_RESET } from '../../../../contsants/fileConstants';
import ConfirmationDialog from '../../../elements/ConfirmationDialog';

const CreateDisaster = ({ isEdit }) => {

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
    const siteDetails = useSelector((state) => state.site);
    const { userSiteList } = siteDetails
    const disasterDetails = useSelector((state) => state.disaster);
    const { disasterDetail, createDisaster, disasterUpdate } = disasterDetails
    const fileDetails = useSelector((state) => state.file);
    const [isLoading, setIsLoading] = useState(false)

    const [photoData, setPhotoData] = useState('')
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
            uploads: pdfFiles,
            token: JSON.parse(userInfo).token
        }

        if (isEdit) {
            params.id = id
            params.photoId = disasterDetail.disasterInfo?.photoId == null ? '' : disasterDetail.disasterInfo?.photoId
            params.galleryId = disasterDetail.disasterInfo?.galleryId
            dispatch(updateDisaster(params))
        } else {
            dispatch(disasterCreate(params))
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
                dispatch(getDisasterDetail(id, userSiteList?.siteList[0].id))
            }
        } 
    }, [userSiteList.siteList])

    useEffect(() => {
        if (isEdit) {
            if (disasterDetail.disasterInfo) {
                if (disasterDetail.disasterInfo.imageList.length > 0) {
                    manipulateFileData(disasterDetail.disasterInfo.imageList, 'image/png', userInfo, setPhotoData, null, setPdfData)
                }

                if (disasterDetail.disasterInfo.fileList.length > 0) {
                    manipulateFileData(disasterDetail.disasterInfo.fileList, 'application/pdf', userInfo, setPhotoData, null, setPdfData)
                }

                setTitle(disasterDetail.disasterInfo.title)
                setRange(disasterDetail.disasterInfo.browseRange)
                setEditorHtml(disasterDetail.disasterInfo.description)
                setEnabled(disasterDetail.disasterInfo.isActive)
            }
        }
    },[disasterDetail])

    useEffect(() => {
        if (isEdit && disasterUpdate) {
            setIsLoading(disasterUpdate.loading)
            if (disasterUpdate.error) {
                toast.error(disasterUpdate.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }   else if (disasterUpdate.success == true) {
                history('/dashboard/disaster')
            }
        } else if (createDisaster) {
            setIsLoading(createDisaster.loading)
            if (createDisaster.error) {
                toast.error(createDisaster.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }   else if (createDisaster.success == true) {
                history('/dashboard/disaster')
            }
        }
    },[createDisaster, disasterUpdate])

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
                        {isEdit ?
                        t("Disaster")
                        :
                        t("Edit Disaster")
                        }
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
                    {isEdit && <PhotoEditDisplay data={photoData} type={"Disaster"} 
                    categoryId={disasterDetail?.disasterInfo?.disasterId} 
                    setDeleteModal={setDeleteModal} setData={setSelectedData}/> }

                    <PdfUpload pdfFiles={pdfFiles} setPdfFiles={setPdfFiles} />
                    {isEdit && <PdfEditDisplay data={pdfData} type={"Disaster"} 
                    categoryId={disasterDetail?.disasterInfo?.disasterId} 
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

export default CreateDisaster