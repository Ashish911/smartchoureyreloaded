import React, { useEffect, useState } from 'react'
import AdvancedTable from '../../elements/AdvancedTable'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import CreateSafetyDeclaration from './SafetyDeclaration/CreateSafetyDeclaration'
import { useDispatch, useSelector } from 'react-redux';
import { listSiteCode } from '../../../actions/siteActions';
import { getSafetyDeclarationList } from '../../../actions/safetyDeclarationAction';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { CREATE_SAFETY_DECLARATION_RESET, CREATE_SAFETY_DECLARATION_SUCCESS, SAFETY_DECLARATION_DELETE_RESET, UPDATE_SAFETY_DECLARATION_RESET } from '../../../contsants/safetyDeclarationConstants';
import { toastUI } from '../../../util/util';

const SafetyDeclaration = () => {

    const dispatch = useDispatch();
    var siteId = localStorage.getItem('siteId');
    const safetyDetails = useSelector((state) => state.safetyDeclaration);
    const { safetyList, safetyDelete, safetyUpdate, safetyCreate } = safetyDetails
    const [showModal, setShowModal] = useState(false)
    const [deleteModal, setDeleteModal] = useState(false)
    const [data, setData] = useState([]);
    const [isEdit, setIsEdit] = useState(false)
    const [params, setParams] = useState({})
    const [selectedData, setSelectedData] = useState({})
    const [isLoading, setIsLoading] = useState(false)

    let safetyData = []

    const createFunction = () => {
        if (showModal == true) {
            setIsEdit(false)
            setShowModal(false)
        } else {
            setIsEdit(false)
            setShowModal(true)
        }
    }

    const detailFunction = (row) => {
        if (showModal == true) {
            setParams({})
            setIsEdit(false)
            setShowModal(false)
        } else {
            setParams({
                title: row.original.title,
                id: row.original.id,
                description: row.original.description,
                active: row.original.enable
            })
            setIsEdit(true)
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
                siteId: row.original.siteId
            })
            setDeleteModal(true)
        }
    }
    
    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getSafetyDeclarationList(siteId))
        }
    }, [siteId])

    useEffect(() => {
        if (safetyDelete) {
            let resp = toastUI(safetyDelete, setIsLoading, "Safety Declaration", "deleted.")
            if (resp) {
                dispatch({ type: SAFETY_DECLARATION_DELETE_RESET })
            }
        }
    }, [safetyDelete])

    useEffect(() => {
        if (safetyUpdate) {
            let resp = toastUI(safetyUpdate, setIsLoading, "Safety Declaration", "updated.")
            if (resp) {
                dispatch({ type: UPDATE_SAFETY_DECLARATION_RESET })
            }
        }
    }, [safetyUpdate])

    useEffect(() => {
        if (safetyCreate) {
            let resp = toastUI(safetyCreate, setIsLoading, "Safety Declaration", "created.")
            if (resp) {
                dispatch({ type: CREATE_SAFETY_DECLARATION_RESET })
            }
        }
    }, [safetyCreate])

    useEffect(() => {
        if (safetyList) {
            setIsLoading(safetyList.loading)
            if (safetyList.error) {
                toast.error(safetyList.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (safetyList.safetyDeclarationList){
                safetyData = []
                let no = 1
                safetyList.safetyDeclarationList.map((safety) => {
                    let obj = {
                        sNo: no,
                        title: safety.title,
                        enable: safety.isActive,
                        id: safety.siteDeclarationId,
                        description: safety.description,
                        siteId: safety.siteId
                    }
                    no++
                    safetyData.push(obj)
                })
            }
            setData(safetyData)
        }
    }, [safetyList])

    const columns = React.useMemo(
        () => [
            {
                Header: 'S.No',
                accessor: 'sNo', 
            },
            {
                Header: 'Title',
                accessor: 'title',
            },
            {
                Header: 'Enabled',
                accessor: 'enable',
            },
        ],
        []
    )

    const renderSafetyDeclaration = () => {
        if (isEdit) {
            return  <><CreateSafetyDeclaration createFunction={createFunction} isEdit={isEdit} data={params} /></>
        } else {
            return  <><CreateSafetyDeclaration createFunction={createFunction} isEdit={isEdit} /></>
        }
    }

    return (
        <>
            {isLoading && <Loader />}
            <AdvancedTable columns={columns} data={data} 
            createText={'Create Safety Declaration'} createFunction={createFunction} 
            enableSelect={true} enableEdit={false} detailFunction={detailFunction}
            deleteFunction={deleteFunction} multiDeleteFunction={deleteFunction}/>
            {showModal ? renderSafetyDeclaration() : null }
            {deleteModal ? (
                <>
                    <ConfirmationDialog deleteFunction={deleteFunction} text={'Safety Declaration'} params={selectedData} type={'SAFEDEC'} />
                </>
            ) : null }
        </>
    )
}

export default SafetyDeclaration