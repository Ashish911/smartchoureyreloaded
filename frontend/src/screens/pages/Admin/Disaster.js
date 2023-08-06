import React, { useEffect, useState } from 'react'
import AdvancedTable from '../../elements/AdvancedTable'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import {
    useLocation,
    useNavigate,
    Link
} from "react-router-dom";
import { useTranslation } from 'react-i18next'
import { useDispatch, useSelector } from 'react-redux';
import { deleteMultipleDisaster, getDisasterList } from '../../../actions/disasterAction';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { toastUI } from '../../../util/util';
import { CREATE_DISASTER_RESET, DISASTER_DELETE_RESET, MULTIPLE_DISASTER_DELETE_RESET, UPDATE_DISASTER_RESET } from '../../../contsants/disasterConstants';

const Disaster = () => {

    const {t} = useTranslation()
    const dispatch = useDispatch();
    let history = useNavigate();
    const [deleteModal, setDeleteModal] = useState(false)
    var siteId = localStorage.getItem('siteId');
    const disasterInfo = useSelector((state) => state.disaster);
    const { disasterList, multipleDisasterDelete, createDisaster, disasterUpdate } = disasterInfo
    const [data, setData] = useState([]);
    const [selectedData, setSelectedData] = useState({})
    const [isLoading, setIsLoading] = useState(false)
    let toasted = false
    var userInfo = localStorage.getItem('userInfo');

    let disasterData = []

    const createFunction = () => {
        history('/dashboard/createDisaster')
    }

    const detailFunction = (row) => {
        history('/dashboard/disasterDetail?id=' + row.original.id)
    }

    const editFunction = (row) => {
        history('/dashboard/editDisaster?id=' + row.original.id)
    }

    const multiDeleteFunction = (row) => {
        if (deleteModal == true) {
            setSelectedData({})
            setDeleteModal(false)
        } else {
            let multiId = []
            if (row[0] == undefined) {
                multiId.push(row.original.id)
            } else {
                row?.map((map) => {
                    multiId.push(map.original.id)
                })
            }
    
            let params = {
                id: multiId,
                siteId: siteId,
                token: JSON.parse(userInfo).token
            }
            setSelectedData(params)
            setDeleteModal(true)
        }
    }


    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getDisasterList(siteId))
        }
    }, [siteId])

    useEffect(() => {
        if (multipleDisasterDelete) {
            let resp = toastUI(multipleDisasterDelete, setIsLoading, "Disaster", "deleted.")
            if (resp) {
                dispatch({ type: MULTIPLE_DISASTER_DELETE_RESET })
            }
        }
    }, [multipleDisasterDelete])

    useEffect(() => {
        if (disasterUpdate) {
            if (disasterUpdate.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(disasterUpdate, setIsLoading, "Disaster", "updated.")
                if (resp) {
                    dispatch({ type: UPDATE_DISASTER_RESET })
                }
                toasted = true
            }
        }
    }, [disasterUpdate])

    useEffect(() => {
        if (createDisaster) {
            if (createDisaster.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(createDisaster, setIsLoading, "Disaster", "created.")
                if (resp) {
                    dispatch({ type: CREATE_DISASTER_RESET })
                }
                toasted = true
            }
        }
    }, [createDisaster])

    useEffect(() => {
        if (disasterList) {
            setIsLoading(disasterList.loading)
            if (disasterList.error) {
                toast.error(disasterList.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (disasterList.list){
                disasterData = []
                let no = 1
                disasterList.list.map((disaster) => {
                    let obj = {
                        sNo: no,
                        title: disaster.title,
                        enable: disaster.isActive,
                        id: disaster.disasterId
                    }
                    no++
                    disasterData.push(obj)
                })
            }
            setData(disasterData)
        } 
    }, [disasterList])

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

    return (
        <>
            {isLoading && <Loader />}
            <AdvancedTable columns={columns} data={data} 
            createText={'Create Disaster'} createFunction={createFunction} 
            enableSelect={true} enableEdit={true} editFunction={editFunction} 
            detailFunction={detailFunction} multiDeleteFunction={multiDeleteFunction}/>
            {deleteModal ? (
                <>
                    <ConfirmationDialog deleteFunction={multiDeleteFunction} text={'Disaster'} params={selectedData} type={'DISASTER'} />
                </>
            ) : null }
        </>
    )
}

export default Disaster