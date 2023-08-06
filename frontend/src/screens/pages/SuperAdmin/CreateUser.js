import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next'
import { createUser } from '../../../actions/userActions';

const CreateUser = ({createFunction}) => {

    const dispatch = useDispatch();

    const {t} = useTranslation()

    const [userName, setUserName] = useState('');
    const [companyName, setCompanyName] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [selectedValue, setSelectedValue] = useState("");

    const siteDetail = useSelector((state) => state.site);
    const { userSiteList } = siteDetail

    const userCreateDetail = useSelector((state) => state.userCreate);

    const saveFunction = () => {
        let siteName = userSiteList.siteList.find((item) => item.id == selectedValue).siteName
        dispatch(createUser(userName, companyName, phoneNumber, selectedValue, siteName))
    }

    useEffect(() => {
        if (userSiteList.siteList) {
            if (selectedValue == '') {
                setSelectedValue(userSiteList.siteList[0].id)
            }
        }
    }, [userSiteList.siteList])

    useEffect(() => {
        if(userCreateDetail) {
            if(userCreateDetail.success) {
                createFunction()
            }
        }
    }, [userCreateDetail])

    return (
        <>
            <div
                className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
                <div className="relative w-5/6 my-6 mx-auto max-w-3xl">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-3xl font-semibold">
                        {t("Create User")}
                        </h3>
                        <button
                            className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                            onClick={createFunction}
                        >
                            <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">
                            ×
                            </span>
                        </button>
                        </div>
                        <div className="relative px-6 pt-6 pb-2 sm:p-6 flex ">
                            <div class="flex flex-1 flex-col sm:flex-row space-y-1 sm:items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="userName" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Username')}:*</label> 
                                    <input
                                        type="text"
                                        id="userName"
                                        autoFocus
                                        value={userName}
                                        onChange={(e) => setUserName(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="companyName" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Company Name')}:*</label>
                                    <input
                                        type="text"
                                        id="companyName"
                                        autoFocus
                                        value={companyName}
                                        onChange={(e) => setCompanyName(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                        </div>
                        <div className="relative px-6 pt-2 pb-6 sm:p-6 flex ">
                            <div class="flex flex-1 flex-col sm:flex-row space-y-1 sm:items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="phoneNo" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Phone Number')}:*</label>
                                    <input
                                        type="text"
                                        id="phoneNo"
                                        autoFocus
                                        value={phoneNumber}
                                        onChange={(e) => setPhoneNumber(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="siteName" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Site Name')}:*</label> 
                                    <select
                                        value={selectedValue}
                                        onChange={event => setSelectedValue(event.target.value)}
                                        className="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"
                                    >
                                        {userSiteList.siteList && userSiteList.siteList.map((product) => (
                                            <option value={product.id}>{product.siteName}</option>
                                        ))}
                                    </select>
                                </div>
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

export default CreateUser