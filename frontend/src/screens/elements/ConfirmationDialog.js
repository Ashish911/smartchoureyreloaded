import React from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { deleteSafetyDeclaration } from '../../actions/safetyDeclarationAction';
import { deleteSubAdmin } from '../../actions/subAdminAction';
import { deleteMultipleChoureyOne, deleteMultipleChoureyTwo } from '../../actions/choureyAction';
import { deleteMultipleDisaster } from '../../actions/disasterAction';
import { deleteDeviceMapping } from '../../actions/deviceAction';
import { deleteMediaFile } from '../../actions/fileAction';
import { deleteCommonContents } from '../../actions/commonContentsAction';
import { deleteSiteCode } from '../../actions/siteActions';

const ConfirmationDialog = ({deleteFunction, text, params , type}) => {

    const dispatch = useDispatch();

    function deleteAccToType () {
        if (type == "SAFEDEC") {
            dispatch(deleteSafetyDeclaration(params))
        } else if (type == "SUBADMIN") {
            dispatch(deleteSubAdmin(params))
        } else if (type == "CHOUREYONE") {
            dispatch(deleteMultipleChoureyOne(params))
        } else if (type == "CHOUREYTWO") {
            dispatch(deleteMultipleChoureyTwo(params))
        } else if (type == "DISASTER") {
            dispatch(deleteMultipleDisaster(params))
        } else if (type == "DEVMAP") {
            dispatch(deleteDeviceMapping(params))
        } else if (type == "MEDIADELETE") {
            dispatch(deleteMediaFile(params))
        } else if (type == "COMMON") {
            dispatch(deleteCommonContents(params))
        } else if (type == "SITECODE") {
            dispatch(deleteSiteCode(params))
        }
        deleteFunction()
    }

    return (
        <>
            <div
                className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
                <div className="relative w-5/12 my-6 mx-auto max-w-3xl">
                    <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                        <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                            <div class="md:flex items-center">
                                <div class="mt-4 md:mt-0 md:ml-6 text-center md:text-left">
                                <p class="font-bold">Delete {text}</p>
                                <p class="text-sm text-gray-700 mt-1">Are you sure you want to delete this item?</p>
                                </div>
                            </div>
                            <div class="text-center md:text-right mt-4 md:flex md:justify-end">
                                <button onClick={() => deleteAccToType()} class="block w-full md:inline-block md:w-auto px-4 py-3 md:py-2 bg-red-200 text-red-700 rounded-lg font-semibold text-sm md:ml-2 md:order-2">Delete</button>
                                <button onClick={deleteFunction} class="block w-full md:inline-block md:w-auto px-4 py-3 md:py-2 bg-gray-200 rounded-lg font-semibold text-sm mt-4
                                md:mt-0 md:order-1">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
    )
}

export default ConfirmationDialog