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
import { getChoureyTwoList } from '../../../actions/choureyAction';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { toastUI } from '../../../util/util';
import { CHOUREY_TWO_DELETE_RESET, CREATE_CHOUREY_TWO_RESET, MULTIPLE_CHOUREY_TWO_DELETE_RESET, UPDATE_CHOUREY_TWO_RESET } from '../../../contsants/choureyConstants';

const ChoureyTwo = () => {

    const {t} = useTranslation()
    const dispatch = useDispatch();
    let history = useNavigate();
    const [deleteModal, setDeleteModal] = useState(false)
    var siteId = localStorage.getItem('siteId');
    const choureyInfo = useSelector((state) => state.chourey);
    const { choureyTwoList, multipleChoureyTwoDelete, createChoureyTwo, choureyTwoUpdate } = choureyInfo
    const [data, setData] = useState([]);
    const [selectedData, setSelectedData] = useState({})
    const [isLoading, setIsLoading] = useState(false)
    let toasted = false

    let choureTwoData = []

    const createFunction = () => {
        history('/dashboard/createChoureyTwo')
    }

    const detailFunction = (row) => {
        history('/dashboard/choureyTwoDetail?id=' + row.original.id)
    }

    const editFunction = (row) => {
        history('/dashboard/createChoureyTwoEdit?id=' + row.original.id)
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
            dispatch(getChoureyTwoList(siteId))
        }
    }, [siteId])

    useEffect(() => {
        if (multipleChoureyTwoDelete) {
            let resp = toastUI(multipleChoureyTwoDelete, setIsLoading, "Chourey Two", "deleted.")
            if (resp) {
                dispatch({ type: MULTIPLE_CHOUREY_TWO_DELETE_RESET })
            }
        }
    }, [multipleChoureyTwoDelete])

    useEffect(() => {
        if (choureyTwoUpdate) {
            if (choureyTwoUpdate.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(choureyTwoUpdate, setIsLoading, "Chourey Two", "updated.")
                if (resp) {
                    dispatch({ type: UPDATE_CHOUREY_TWO_RESET })
                }
                toasted = true
            }
        }
    }, [choureyTwoUpdate])

    useEffect(() => {
        if (createChoureyTwo) {
            if (createChoureyTwo.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(createChoureyTwo, setIsLoading, "Chourey Two", "created.")
                if (resp) {
                    dispatch({ type: CREATE_CHOUREY_TWO_RESET })
                }
                toasted = true
            }
        }
    }, [createChoureyTwo])

    useEffect(() => {
        if (choureyTwoList) {
            setIsLoading(choureyTwoList.loading)
            if (choureyTwoList.error) {
                toast.error(choureyTwoList.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (choureyTwoList.list){
                choureTwoData = []
                let no = 1
                choureyTwoList.list.map((chourey) => {
                    let obj = {
                        sNo: no,
                        title: chourey.title,
                        enable: chourey.isActive,
                        siteId: chourey.siteId,
                        id: chourey.choureyTwoId
                    }
                    no++
                    choureTwoData.push(obj)
                })
            }
            setData(choureTwoData)
        }
    }, [choureyTwoList])

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
        detailFunction={detailFunction} multiDeleteFunction={multiDeleteFunction}/>
        {deleteModal ? (
            <>
                <ConfirmationDialog deleteFunction={multiDeleteFunction} text={'Chourey Two'} params={selectedData} type={'CHOUREYTWO'} />
            </>
        ) : null }
    </>
    )
}

export default ChoureyTwo