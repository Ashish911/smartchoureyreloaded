import React, { useEffect, useState, useRef } from 'react'
import AdvancedTable from '../../elements/AdvancedTable'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import {
    useLocation,
    useNavigate,
    Link
} from "react-router-dom";
import { useTranslation } from 'react-i18next'
import { useDispatch, useSelector } from 'react-redux';
import { deleteMultipleChoureyOne, getChoureyOneList } from '../../../actions/choureyAction';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { CHOUREY_ONE_DELETE_REQUEST, CHOUREY_ONE_DELETE_RESET, CHOUREY_ONE_DELETE_SUCCESS, CREATE_CHOUREY_ONE_RESET, MULTIPLE_CHOUREY_ONE_DELETE_RESET, UPDATE_CHOUREY_ONE_RESET } from '../../../contsants/choureyConstants';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { toastUI } from '../../../util/util';

const ChoureyOne = () => {

    const {t} = useTranslation()
    const dispatch = useDispatch();
    let history = useNavigate();
    const [deleteModal, setDeleteModal] = useState(false)
    var siteId = localStorage.getItem('siteId');
    const choureyInfo = useSelector((state) => state.chourey);
    const { choureyOneList, multipleChoureyOneDelete, choureyOneUpdate, createChoureyOne } = choureyInfo
    const [data, setData] = useState([]);
    const [selectedData, setSelectedData] = useState({})
    const [progress, setProgress] = useState(0)
    const [isLoading, setIsLoading] = useState(false)
    let toasted = false

    let choureOneData = []

    const createFunction = () => {
        history('/dashboard/createChoureyOne')
    }

    const detailFunction = (row) => {
        history('/dashboard/choureyOneDetail?id=' + row.original.id)
    }

    const editFunction = (row) => {
        history('/dashboard/createChoureyOneEdit?id=' + row.original.id)
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
                siteId: siteId
            }

            setSelectedData(params)
            setDeleteModal(true)
        }
    }

    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getChoureyOneList(siteId))
        }
    }, [siteId])

    useEffect(() => {
        if (multipleChoureyOneDelete) {
            let resp = toastUI(multipleChoureyOneDelete, setIsLoading, "Chourey One", "deleted.")
            if (resp) {
                dispatch({ type: MULTIPLE_CHOUREY_ONE_DELETE_RESET })
            }
        }
    }, [multipleChoureyOneDelete])

    useEffect(() => {
        if (choureyOneUpdate) {
            if (choureyOneUpdate.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(choureyOneUpdate, setIsLoading, "Chourey One", "updated.")
                if (resp) {
                    dispatch({ type: UPDATE_CHOUREY_ONE_RESET })
                }
                toasted = true
            }
        }
    }, [choureyOneUpdate])

    useEffect(() => {
        if (createChoureyOne) {
            if (createChoureyOne.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(createChoureyOne, setIsLoading, "Chourey One", "created.")
                if (resp) {
                    dispatch({ type: CREATE_CHOUREY_ONE_RESET })
                }
                toasted = true
            }
        }
    }, [createChoureyOne])

    useEffect(() => {
        if (choureyOneList) {
            setIsLoading(choureyOneList.loading)
            if (choureyOneList.error) {
                toast.error(choureyOneList.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (choureyOneList.list){
                choureOneData = []
                let no = 1
                choureyOneList.list.map((chourey) => {
                    let obj = {
                        sNo: no,
                        title: chourey.title,
                        enable: chourey.isActive,
                        siteId: siteId,
                        id: chourey.choureyOneId
                    }
                    no++
                    choureOneData.push(obj)
                })
            }
            setData(choureOneData)
        }
    }, [choureyOneList])

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
            createText={'Create Chourey'} createFunction={createFunction} 
            enableSelect={true} enableEdit={true} editFunction={editFunction} 
            detailFunction={detailFunction} multiDeleteFunction={multiDeleteFunction} />
            {deleteModal ? (
                <>
                    <ConfirmationDialog deleteFunction={multiDeleteFunction} text={'Chourey One'} params={selectedData} type={'CHOUREYONE'} />
                </>
            ) : null }
        </>
    )
}

export default ChoureyOne