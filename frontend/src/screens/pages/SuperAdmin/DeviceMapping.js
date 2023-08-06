import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import GlobalTable from '../../elements/GlobalTable'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import {
    useNavigate,
} from "react-router-dom";
import EditDeviceMapping from './Mapping/EditDeviceMapping';
import AssignDevice from './Mapping/AssignDevice';
import { useDispatch, useSelector } from 'react-redux';
import { getDeviceList } from '../../../actions/deviceAction';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { toastUI } from '../../../util/util';
import { ASSIGN_DEVICE_MAPPING_LIST_RESET, DELETE_DEVICE_MAPPING_LIST_RESET, UPDATE_DEVICE_MAPPING_LIST_RESET } from '../../../contsants/deviceConstants';

const DeviceMapping = () => {

    let history = useNavigate();
    const dispatch = useDispatch();
    var siteId = localStorage.getItem('siteId');

    const [currentDiv, setCurrentDiv] = useState('unassigned')

    const [showModal, setShowModal] = useState(false)
    const [deleteModal, setDeleteModal] = useState(false)
    const [assignModal, setAssignModal] = useState(false)

    const deviceDetails = useSelector((state) => state.device);
    const { deviceList, deleteDevice, deviceAssign, deviceUpdate } = deviceDetails
    const [assignedData, setAssignedData] = useState([]);
    const [unassignedData, setUnAssignedData] = useState([])
    const [selectedData, setSelectedData] = useState({});
    const [isLoading, setIsLoading] = useState(false)

    let deviceData = []

    const {t} = useTranslation()

    const editFunction = (row) => {
        if (showModal == true) {
            setSelectedData({})
            setShowModal(false)
        } else {
            setSelectedData({
                id: row.original.id,
                phoneNumber: row.original.phoneNo,
                uniqueId: row.original.uniqueId
            })
            setShowModal(true)
        }
    }

    const deleteFunction = (row) => {
        if (deleteModal == true) {
            setSelectedData({})
            setDeleteModal(false)
        } else {
            setSelectedData({
                id: row.original.id
            })
            setDeleteModal(true)
        }
    }

    const assignFunction = (row) => {
        if (assignModal == true) {
            setSelectedData({})
            setAssignModal(false)
        } else {
            setSelectedData({
                id: row.original.id
            })
            setAssignModal(true)
        }
    }

    useEffect(() => {
        dispatch(getDeviceList())
    }, [])

    useEffect(() => {
        if (deviceList) {
            setIsLoading(deviceList.loading)
            if (deviceList.error) {
                toast.error(deviceList.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            manipulateData(deviceList, "assignedDevices", setAssignedData)
            manipulateData(deviceList, "unassignedDevices", setUnAssignedData)
        }
    }, [deviceList])

    function manipulateData(list, type, setData) {
        if (list.list){
            deviceData = []
            let no = 1
            deviceList.list[type].map((device) => {
                let obj = {
                    sNo: no,
                    phoneNo: device.phoneNumber,
                    deviceType: device.deviceType,
                    id: device.deviceRegistrationId,
                    uniqueId: device.deviceUniqueId
                }
                no++
                deviceData.push(obj)
            })
        }
        setData(deviceData)
    }

    useEffect(() => {
        if (deleteDevice) {
            let resp = toastUI(deleteDevice, setIsLoading, "Device Mapping", "deleted.")
            if (resp) {
                dispatch({ type: DELETE_DEVICE_MAPPING_LIST_RESET })
            }
        }
    }, [deleteDevice])

    useEffect(() => {
        if (deviceAssign) {
            let resp = toastUI(deviceAssign, setIsLoading, "Device Mapping", "assigned.")
            if (resp) {
                dispatch({ type: ASSIGN_DEVICE_MAPPING_LIST_RESET })
            }
        }
    }, [deviceAssign])

    useEffect(() => {
        if (deviceUpdate) {
            let resp = toastUI(deviceUpdate, setIsLoading, "Device Mapping", "updated.")
            if (resp) {
                dispatch({ type: UPDATE_DEVICE_MAPPING_LIST_RESET })
            }
        }
    }, [deviceUpdate])

    const columns = React.useMemo(
        () => [
            {
                Header: () => (
                    <a>
                        {t('S.NO')}
                    </a>
                ),
                accessor: 'sNo', 
            },
            {
                Header: () => (
                    <a>
                        {t('Phone Number')}
                    </a>
                ),
                accessor: 'phoneNo',
            },
            {
                Header: () => (
                    <a>
                        {t('Device Type')}
                    </a>
                ),
                accessor: 'deviceType',
            }
        ],
        []
    )

    return (
        <>
        {isLoading && <Loader />}
        <div>
            <ul className='flex ml-1.5 mb-1.5'>
                <li className={currentDiv == 'unassigned' ? "current bg-cyan-500 text-white p-2 rounded-l border" : "bg-white text-black p-2 rounded-l border"} id='unassigned'><a className='cursor-pointer' onClick={() => setCurrentDiv('unassigned')}>{t('Unassigned Site')}</a></li>
                <li className={currentDiv == 'assigned' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='assigned'><a className='cursor-pointer' onClick={() => setCurrentDiv('assigned')}>{t('Assigned Site')}</a></li>
            </ul>
        </div>
        { currentDiv == 'unassigned' ?
        <GlobalTable columns={columns} data={unassignedData} 
            enableEdit={true} editFunction={editFunction}
            enableDelete={true} deleteFunction={deleteFunction} 
            enableAssign={true} assignFunction={assignFunction} />
        :
        <GlobalTable columns={columns} data={assignedData} 
            enableEdit={true} editFunction={editFunction}
            enableDelete={true} deleteFunction={deleteFunction} />
        }
        {showModal ? (
            <>
                <EditDeviceMapping editFunction={editFunction} selectedData={selectedData} />
            </>
        ) : null }
        {deleteModal ? (
            <>
                <ConfirmationDialog deleteFunction={deleteFunction} text={'Device Mapping'} params={selectedData} type={'DEVMAP'} />
            </>
        ) : null }
        {assignModal ? (
            <>
                <AssignDevice assignFunction={assignFunction} selectedData={selectedData}/>
            </>
        ) : null }
        </>
    )
}

export default DeviceMapping