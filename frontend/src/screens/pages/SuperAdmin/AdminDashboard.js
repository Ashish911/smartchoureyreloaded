import React, { useState } from 'react'
import { useTranslation } from 'react-i18next'
import GlobalTable from '../../elements/GlobalTable'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import {
    useNavigate,
} from "react-router-dom";
import GenerateSiteCode from './GenerateSiteCode';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { listAllSiteCode } from '../../../actions/siteActions';
import { listSiteCode } from '../../../actions/siteActions';
import { SITE_CODE_DELETE_RESET } from '../../../contsants/siteConstants';
import Loader from '../../elements/Loader';
import { toastUI } from '../../../util/util';

const AdminDashboard = () => {

    // Const/Variables
    let history = useNavigate();
    const [currentDiv, setCurrentDiv] = useState('unassigned')
    const [showModal, setShowModal] = useState(false)
    const [deleteModal, setDeleteModal] = useState(false)
    const dispatch = useDispatch();
    const siteDetails = useSelector((state) => state.site);
    const { siteList, siteCodeDelete } = siteDetails
    const {t} = useTranslation()
    const [assignedData, setAssignedData] = useState([]);
    const [unAssignedData, setUnAssignedData] = useState([]);
    const [selectedData, setSelectedData] = useState({});
    const [isLoading, setIsLoading] = useState(false)

    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin

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
                id: row.original.id
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
                        {t('Site Code')}
                    </a>
                ),
                accessor: 'siteCode',
            }
        ],
        []
    )

    useEffect(() => {
        dispatch(listAllSiteCode());
        dispatch(listSiteCode(userInfo.id))
    }, [])

    useEffect(() => {
        if (siteList.assigned && siteList.unassigned) {
            setAssignedData(manipulateData(siteList.assigned))
            setUnAssignedData(manipulateData(siteList.unassigned))
        }
        // Something related to data
    },[siteList])

    const manipulateData = (code) => {
        let siteData = []
            let no = 1
            code.map((site) => {
                let obj = {
                    sNo: no,
                    siteCode: site.siteCode,
                    id: site.siteCodeId
                }
                no++
                siteData.push(obj)
        })
        return siteData
    }

    useEffect(() => {
        if (siteCodeDelete) {
            let resp = toastUI(siteCodeDelete, setIsLoading, "Site Code", "deleted.")
            if (resp) {
                dispatch({ type: SITE_CODE_DELETE_RESET })
            }
        }
    }, [siteCodeDelete])

    return (
        <>
        {isLoading && <Loader />}
        <div>
            <ul className='flex ml-1.5 mb-1.5'>
                <li className={currentDiv == 'unassigned' ? "current bg-cyan-500 text-white p-2 rounded-l border" : "bg-white text-black p-2 rounded-l border"} id='unassigned'><a className='cursor-pointer' onClick={() => setCurrentDiv('unassigned')}>{t('Unassigned Code')}</a></li>
                <li className={currentDiv == 'assigned' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='assigned'><a className='cursor-pointer' onClick={() => setCurrentDiv('assigned')}>{t('Assigned Code')}</a></li>
            </ul>
        </div>
        { currentDiv == 'unassigned' ?
        <GlobalTable columns={columns} data={unAssignedData} 
            createText={t('Generate Code')} createFunction={createFunction} 
            enableDetail={false} enableDelete={true} deleteFunction={deleteFunction} />
        :
        <GlobalTable columns={columns} data={assignedData} 
            createText={t('Generate Code')} createFunction={createFunction} 
            enableDetail={false} enableDelete={true} deleteFunction={deleteFunction} />
        }
        {showModal ? (
            <>
                <GenerateSiteCode createFunction={createFunction}/>
            </>
        ) : null }
        {deleteModal ? (
            <>
                <ConfirmationDialog deleteFunction={deleteFunction} text={'Site Code'} params={selectedData} type={'SITECODE'} />
            </>
        ) : null }
        </>
    )
}

export default AdminDashboard