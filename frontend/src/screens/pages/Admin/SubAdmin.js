import React, { useEffect, useState } from 'react'
import GlobalTable from '../../elements/GlobalTable'
import CreateSubAdmin from './SubAdmin/CreateSubadmin'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import { useTranslation } from 'react-i18next'
import { useDispatch, useSelector } from 'react-redux';
import { getSubAdminList } from '../../../actions/subAdminAction';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { toastUI } from '../../../util/util';
import { SUB_ADMIN_ASSIGN_RESET, SUB_ADMIN_DELETE_RESET } from '../../../contsants/subAdminConstants'

const SubAdmin = () => {

    const {t} = useTranslation()
    const dispatch = useDispatch();
    const [showModal, setShowModal] = useState(false)
    const [deleteModal, setDeleteModal] = useState(false)
    var siteId = localStorage.getItem('siteId');
    const subAdminInfo = useSelector((state) => state.subAdmin);
    const { subAdminList, subAdminDelete, subAdminAssign } = subAdminInfo
    const [data, setData] = useState([]);
    const [selectedData, setSelectedData] = useState({})
    const [isLoading, setIsLoading] = useState(false)

    var userInfo = localStorage.getItem('userInfo');

    let subAdminData = []

    const createFunction = () => {
        if (showModal == true) {
            setShowModal(false)
        } else {
            setShowModal(true)
        }
    }

    const deleteFunction = (row) => {
        if (deleteModal == true) {
            setSelectedData({})
            setDeleteModal(false)
        } else {
            setSelectedData({
                id: row.original.id,
                siteId: row.original.siteId,
                token: JSON.parse(userInfo).token
            })
            setDeleteModal(true)
        }
    }

    const columns = React.useMemo(
        () => [
            {
                Header: 'S.No',
                accessor: 'sNo', 
            },
            {
                Header: 'Site Name',
                accessor: 'siteName',
            },
            {
                Header: 'FullName',
                accessor: 'fullName',
            },
            {
                Header: 'Address',
                accessor: 'address',
            },
            {
                Header: 'Email',
                accessor: 'email',
            },
            {
                Header: 'Mobile Number',
                accessor: 'mobileNumber',
            }
        ],
        []
    )

    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getSubAdminList(siteId))
        }
    }, [siteId])

    useEffect(() => {
        if (subAdminDelete) {
            let resp = toastUI(subAdminDelete, setIsLoading, "Sub Admin", "deleted.")
            if (resp) {
                dispatch({ type: SUB_ADMIN_DELETE_RESET })
            }
        }
    }, [subAdminDelete])

    useEffect(() => {
        if (subAdminAssign) {
            let resp = toastUI(subAdminAssign, setIsLoading, "Sub Admin", "assigned.")
            if (resp) {
                dispatch({ type: SUB_ADMIN_ASSIGN_RESET })
            }
        }
    }, [subAdminAssign])

    useEffect(() => {
        if (subAdminList) {
            setIsLoading(subAdminList.loading)
            if (subAdminList.error) {
                toast.error(subAdminList.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (subAdminList.list){
                subAdminData = []
                let no = 1
                subAdminList.list.map((user) => {
                    let obj = {
                        sNo: no,
                        siteName: user.siteName,
                        fullName: user.fullName,
                        address: user.fullAddress,
                        email: user.email,
                        id: user.employeeId,
                        siteId: user.siteId
                    }
                    no++
                    subAdminData.push(obj)
                })
            }
            setData(subAdminData)
        }
    }, [subAdminList])

    return (
        <>
            {isLoading && <Loader />}
            <GlobalTable columns={columns} data={data} 
            createText={'Assign Sub Admin'} createFunction={createFunction}
            enableDelete={true} deleteFunction={deleteFunction}/>
            {showModal ? (
            <>
                <CreateSubAdmin createFunction={createFunction} />
            </>
            ) : null }
            {deleteModal ? (
                <>
                    <ConfirmationDialog deleteFunction={deleteFunction} text={'Sub Admin User'} params={selectedData} type={'SUBADMIN'} />
                </>
            ) : null }
        </>
    )
}

export default SubAdmin