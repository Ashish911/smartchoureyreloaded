import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import {
    useLocation,
    useNavigate,
    Link
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import { toast } from 'react-toastify';
import { validateSiteCode } from '../../../actions/siteActions';

const UserCreateSite = () => {

    let history = useNavigate();
    const dispatch = useDispatch();
    const {t} = useTranslation()
    const [code, setCode] = useState('');
    const [isLoading, setIsLoading] = useState(false)
    const siteDetails = useSelector((state) => state.site);
    const { siteValidation } = siteDetails

    const saveFunction = () => {
        dispatch(validateSiteCode(code));
    }

    useEffect(() => {
        setIsLoading(siteValidation.loading)
        if (siteValidation.error) {
            toast.error(siteValidation.erro, {
                position: toast.POSITION.TOP_RIGHT
            })
        }   else if (siteValidation.validated == true) {
            history('/userDashboard/siteDetail')
        }
    }, [siteValidation])

    return (
        <>
                <div className="relative w-5/6 my-6 mx-auto max-w-3xl">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-3xl font-semibold">
                        {t("Site Code Registration")}
                        </h3>
                        </div>
                        <div className="relative p-6 flex ">
                            <div class="flex md:flex-1 flex-col space-y-1">
                                <label for="text" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Site Code Information')}</label>
                                <input
                                    type="text"
                                    id="text"
                                    value={code}
                                    onChange={(e) => setCode(e.target.value)}
                                    autoFocus
                                    class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                />
                            </div>
                        </div>
                        <div className="flex items-center justify-start p-6 border-t border-solid border-slate-200 rounded-b">
                        <button
                            className="text-white bg-cyan-500 font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                            type="button"
                            onClick={saveFunction}
                        >
                            <p>{t("Register")}</p>
                        </button>
                        </div>
                    </div>
                </div>
        </>
    )
}

export default UserCreateSite