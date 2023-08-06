import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next'
import { MdOutlineAssignmentTurnedIn } from 'react-icons/md'
import { assignDeviceMapping } from '../../../../actions/deviceAction';

const AssignDevice = ({assignFunction, selectedData}) => {

    const dispatch = useDispatch();
    const {t} = useTranslation()

    const siteDetail = useSelector((state) => state.site);
    const { userSiteList } = siteDetail

    const [selectedValue, setSelectedValue] = useState("");
    const deviceDetails = useSelector((state) => state.device);
    const { deviceAssign } = deviceDetails


    const assign = () => {
        let params = {
            fkDeviceRegistrationId: selectedData.id,
            fkSiteId: selectedValue == "" ? userSiteList.siteList[0].id : selectedValue
        }
        dispatch(assignDeviceMapping(params))
    }

    useEffect(() => {
        if (deviceAssign) {
            if (deviceAssign.success) {
                assignFunction()
            }
        }
    }, [deviceAssign])

    return (
        <>
            <div
                className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
                <div className="relative w-5/6 my-6 mx-auto max-w-md">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-3xl font-semibold">
                        {t("Assign Device Mapping")}
                        </h3>
                        <button
                            className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                            onClick={assignFunction}
                        >
                            <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">
                            ×
                            </span>
                        </button>
                        </div>
                        <div className="relative px-6 pb-2 flex ">
                            <div class="flex flex-col space-y-1 p-1">
                                <label for="assignSite" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Storage')}:</label> 
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
                        <div className="flex items-center justify-start p-6 border-t border-solid border-slate-200 rounded-b">
                        <button
                            className="text-white bg-cyan-500 font-bold uppercase py-2 px-2.5 flex justify-center items-center text-md outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                            type="button"
                            onClick={assign}
                        >
                            <MdOutlineAssignmentTurnedIn /> <span className='px-2'>{t("Assign")}</span>
                        </button>
                        </div>
                    </div>
                    </div>
                </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
    )
}

export default AssignDevice