import React, { useCallback, useEffect, useRef, useState } from 'react'
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { useTranslation } from 'react-i18next'
import {
    useNavigate,
    useLocation
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import Loader from '../../../elements/Loader';
import PhotoUpload from '../../../elements/PhotoUpload';
import PhotoEditDisplay from '../../../elements/PhotoEditDisplay';
import { createCommonContents, getCommonContentsDetail, updateCommonContents } from '../../../../actions/commonContentsAction';
import ConfirmationDialog from '../../../elements/ConfirmationDialog';
import { toast } from 'react-toastify';
import { manipulateFileData } from '../../../../util/util';

const CreateCommonContent = ({ isEdit }) => {

    const {t} = useTranslation()

    let history = useNavigate();
    const dispatch = useDispatch();

    const [title, setTitle] = useState("");
    const [editorHtml, setEditorHtml] = useState('');
    const [enabled, setEnabled] = useState(false)
    const [isLoading, setIsLoading] = useState(false)
    const [files, setFiles] = useState([]);

    const contentDetails = useSelector((state) => state.commonContents);
    const { createCommonContent, updateCommonContent, commonContentDetail } = contentDetails

    const [photoData, setPhotoData] = useState('')
    const [deleteModal, setDeleteModal] = useState(false)
    const [selectedData, setSelectedData] = useState({})

    const location = useLocation();
    const params = new URLSearchParams(location.search);
    const id = params.get('id');
    var userInfo = localStorage.getItem('userInfo');

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

    const handleChange = (html) => {
        setEditorHtml(html);
    }

    useEffect(() => {
        if (isEdit) {
            let params = {
                id: id,
                token: JSON.parse(userInfo).token
            }
            dispatch(getCommonContentsDetail(params))
        } 
    },[])

    const submitHandler = (e) => {
        e.preventDefault();
        let params = {
            description: editorHtml,
            title: title,
            isActive: enabled,
            files: files,
            token: JSON.parse(userInfo).token
        }

        if (isEdit) {
            params.id = id
            params.photoId = commonContentDetail.detail.photoId == null ? '' : commonContentDetail.detail.photoId
            // params.galleryId = choureyOneDetail.choureyOneInfo.galleryId

            dispatch(updateCommonContents(params))
        } else {
            dispatch(createCommonContents(params))
        }
    }

    useEffect(() => {
        if (isEdit) {
            if (commonContentDetail.detail) {
                if (commonContentDetail.detail.imageList.length > 0) {
                    manipulateFileData(commonContentDetail.detail.imageList, 'image/png', userInfo, setPhotoData, null, null)
                }
                
                setTitle(commonContentDetail.detail.title)
                setEditorHtml(commonContentDetail.detail.description)
                setEnabled(commonContentDetail.detail.isActive)
            }
        }
    },[commonContentDetail])

    useEffect(() => {
        if (isEdit && updateCommonContent) {
            setIsLoading(updateCommonContent.loading)
            if (updateCommonContent.error) {
                toast.error(updateCommonContent.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }   else if (updateCommonContent.success == true) {
                history('/adminDashboard/commonContents')
            }
        } else if (createCommonContent) {
            setIsLoading(createCommonContent.loading)
            if (createCommonContent.error) {
                toast.error(createCommonContent.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }   else if (createCommonContent.success == true) {
                history('/adminDashboard/commonContents')
            }
        }
    },[createCommonContent, updateCommonContent])

    return (
        <>
            {isLoading && <Loader />}
            <div
                class="flex flex-col overflow-hidden bg-white rounded-md shadow-lg max md:flex-row md:flex-1"
            >
                <div class="flex-1 p-5 bg-white">
                <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                    <h3 className="text-2xl font-semibold">
                    {t("Common Contents.1")}
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
                    {isEdit && <PhotoEditDisplay data={photoData} type={"CommonContents"} 
                    categoryId={null} 
                    setDeleteModal={setDeleteModal} setData={setSelectedData}/> }
                    
                    <div>
                        <button
                            onClick={submitHandler}
                            type="submit"
                            class="w-full px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                        >
                            {isEdit ?
                                t('Update')
                            :
                                t('Create')
                            }
                        </button>
                    </div>
                </form>
                </div>
            </div>
            {/* {deleteModal ? (
                <>
                    <ConfirmationDialog deleteFunction={deleteFunction} text={'Media File'} params={selectedData} type={'MEDIADELETE'} />
                </>
            ) : null } */}
        </>
    )
}

export default CreateCommonContent