import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next'
import { MdOutlineAssignmentTurnedIn } from 'react-icons/md'
import { updateDeviceMapping } from '../../../../actions/deviceAction';

const EditDeviceMapping = ({editFunction, selectedData}) => {

    const dispatch = useDispatch();
    const {t} = useTranslation()

    const [uniqueValue, setUniqueValue] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const deviceDetails = useSelector((state) => state.device);
    const { deviceUpdate } = deviceDetails

    useEffect(() => {
        setPhoneNumber(selectedData.phoneNumber)
    }, [])

    const updateFunction = () => {
        let params = {
            deviceRegistrationId: selectedData.id,
            newPhoneNumber: phoneNumber,
            oldPhoneNumber: selectedData.phoneNumber,
            deviceUniqueId: selectedData.uniqueId
        }
        dispatch(updateDeviceMapping(params))
    }

    useEffect(() => {
        if (deviceUpdate) {
            if (deviceUpdate.success) {
                editFunction()
            }
        }
    }, [deviceUpdate])

    return (
        <>
            <div
                className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
                <div className="relative w-5/6 my-6 mx-auto max-w-md">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                        <h3 className="text-3xl font-semibold">
                        {t("Edit Device Mapping")}
                        </h3>
                        <button
                            className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                            onClick={editFunction}
                        >
                            <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">
                            ×
                            </span>
                        </button>
                        </div>
                        <div className="relative p-6 flex ">
                            <div class="flex w-11/12 flex-col space-y-1 p-1">
                                <label for="text" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Phone Number')}</label>
                                <input
                                    type="text"
                                    id="text"
                                    value={phoneNumber}
                                    onChange={(e) => setPhoneNumber(e.target.value)}
                                    class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                />
                            </div>
                        </div>
                        <div className="flex items-center justify-start p-6 border-t border-solid border-slate-200 rounded-b">
                        <button
                            className="text-white bg-cyan-500 font-bold uppercase py-2 px-2.5 flex justify-center items-center text-md outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 rounded-md shadow"
                            type="button"
                            onClick={updateFunction}
                        >
                            <MdOutlineAssignmentTurnedIn /> <span className='px-2'>{t("Update")}</span>
                        </button>
                        </div>
                    </div>
                    </div>
                </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
    )
}

export default EditDeviceMapping