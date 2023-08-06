import React, { useState, useEffect } from 'react'
import {
    useLocation,
    useNavigate,
    Link
} from "react-router-dom";
import { useTranslation } from 'react-i18next'
import Loader from '../../elements/Loader'
import GlobalTable from '../../elements/GlobalTable'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import { useDispatch, useSelector } from 'react-redux';
import { getCommonContentsList } from '../../../actions/commonContentsAction';
import { toast } from 'react-toastify';
import { CREATE_COMMON_CONTENTS_RESET, DELETE_COMMON_CONTENTS_RESET, UPDATE_COMMON_CONTENTS_RESET } from '../../../contsants/commonContentsConstants';
import { toastUI } from '../../../util/util';

const CommonContent = () => {

    const {t} = useTranslation()
    const dispatch = useDispatch();
    let history = useNavigate();
    const [data, setData] = useState([]);
    const [selectedData, setSelectedData] = useState({});
    const [isLoading, setIsLoading] = useState(false)
    const [deleteModal, setDeleteModal] = useState(false)

    const commonContentsInfo = useSelector((state) => state.commonContents);
    const { listCommonContents, createCommonContent, deleteCommonContent, updateCommonContent } = commonContentsInfo
    
    var userInfo = localStorage.getItem('userInfo');

    let toasted = false
    let commonContentData = []

    const createFunction = () => {
        history('/adminDashboard/createCommonContents')
    }

    const detailFunction = (row) => {
        history('/adminDashboard/commonContentDetails?id=' + row.original.id)
    }

    const deleteFunction = (row) => {
        if (deleteModal == true) {
            setSelectedData({})
            setDeleteModal(false)
        } else {
            setSelectedData({
                id: row.original.id,
                token: JSON.parse(userInfo).token
            })
            setDeleteModal(true)
        }
    }

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
                        {t('Title')}
                    </a>
                ),
                accessor: 'title',
            },
            {
                Header: () => (
                    <a>
                        {t('Active')}
                    </a>
                ),
                accessor: 'enable',
            }
        ],
        []
    )

    useEffect(() => {
        dispatch(getCommonContentsList())
    }, [])

    useEffect(() => {
        if (deleteCommonContent) {
            let resp = toastUI(deleteCommonContent, setIsLoading, "Common Contents", "deleted.")
            if (resp) {
                dispatch({ type: DELETE_COMMON_CONTENTS_RESET })
            }
        }
    }, [deleteCommonContent])

    useEffect(() => {
        if (updateCommonContent && toasted == false) {
            let resp = toastUI(updateCommonContent, setIsLoading, "Common Contents", "updated.")
            if (resp) {
                dispatch({ type: UPDATE_COMMON_CONTENTS_RESET })
            }
            toasted = true
        }
    }, [updateCommonContent])

    useEffect(() => {
        if (createCommonContent && toasted == false) {
            let resp = toastUI(createCommonContent, setIsLoading, "Common Contents", "created.")
            if (resp) {
                dispatch({ type: CREATE_COMMON_CONTENTS_RESET })
            }
            toasted = true
        }
    }, [createCommonContent])

    useEffect(() => {
        if (listCommonContents) {
            setIsLoading(listCommonContents.loading)
            if (listCommonContents.error) {
                toast.error(listCommonContents.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (listCommonContents.list){
                commonContentData = []
                let no = 1
                listCommonContents.list.map((content) => {
                    let obj = {
                        sNo: no,
                        title: content.title,
                        enable: content.isActive,
                        id: content.publicUserboardId
                    }
                    no++
                    commonContentData.push(obj)
                })
            }
            setData(commonContentData)
        }
    }, [listCommonContents])

    return (
        <>
            {isLoading && <Loader />}
            <GlobalTable columns={columns} data={data} 
                createText={t('Create Common Content')} createFunction={createFunction} 
                enableDetail={true} detailFunction={detailFunction} enableDelete={true} deleteFunction={deleteFunction} /> 
            {deleteModal ? (
                <>
                    <ConfirmationDialog deleteFunction={deleteFunction} text={'Common Contents'} params={selectedData} type={'COMMON'} />
                </>
            ) : null }
        </>
    )
}

export default CommonContent