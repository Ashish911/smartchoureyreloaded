import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { FaCogs } from "react-icons/fa"
import { v4 as uuidv4 } from 'uuid';
import { createSiteCode } from '../../../actions/siteActions';
import { useDispatch, useSelector } from 'react-redux';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader';
import { SITE_CODE_CREATE_RESET } from '../../../contsants/siteConstants';

const GenerateSiteCode = ({createFunction}) => {

    // Const/Variables
    const dispatch = useDispatch();
    const {t} = useTranslation()
    const [code, setCode] = useState('');
    const [isLoading, setIsLoading] = useState(false)
    const siteDetails = useSelector((state) => state.site);
    const userLogin = useSelector((state) => state.user);
    const { loading, error, userInfo } = userLogin
    const { siteCode } = siteDetails

    // Functions
    function generateUUID() {
        const uuid = uuidv4();
        setCode(uuid)
    }

    const saveFunction = () => {
        dispatch(createSiteCode(code, userInfo.id, userInfo.id));
    }

    const closePopup = () => {
        dispatch({ type: SITE_CODE_CREATE_RESET })
        createFunction();
    }

    // DOC EFFECTS
    useEffect(() => {
        generateUUID()
    },[])

    useEffect(() => {
        if (siteCode) {
            setIsLoading(siteCode.loading)
            if (siteCode.error) {
                toast.error(siteCode.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }else if (siteCode.success == true) {
                toast.success("Site Code has been successfully created.", {
                    position: toast.POSITION.TOP_RIGHT
                })
                dispatch({ type: SITE_CODE_CREATE_RESET })
                createFunction()
            }
        }
        
    }, [siteCode])

    return (
        <>
            {isLoading && <Loader />}
            <div
                className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
                <div className="relative w-5/6 my-6 mx-auto max-w-3xl">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-3xl font-semibold">
                        {t("Generate Code")}
                        </h3>
                        <button
                            className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                            onClick={closePopup}
                        >
                            <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">
                            ×
                            </span>
                        </button>
                        </div>
                        <div className="relative p-6 flex ">
                            <div class="flex w-11/12 flex-col space-y-1 p-1">
                                <label for="text" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Site Code')}</label>
                                <input
                                    type="text"
                                    id="text"
                                    value={code}
                                    disabled 
                                    class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                />
                            </div>
                            <div class="flex w-1/12 flex-col justify-end space-y-1">
                                <button
                                    className="text-white bg-cyan-500 font-bold uppercase py-3.5 px-2.5 flex justify-center text-md outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                                    type="button"
                                    onClick={generateUUID}
                                >
                                    <FaCogs />
                                </button>
                            </div>
                        </div>
                        <div className="flex items-center justify-start p-6 border-t border-solid border-slate-200 rounded-b">
                        <button
                            className="text-white bg-cyan-500 font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                            type="button"
                            onClick={saveFunction}
                        >
                            <p>{t("Save")}</p>
                        </button>
                        </div>
                    </div>
                    </div>
                </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
    )
}

export default GenerateSiteCode