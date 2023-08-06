import React, { useEffect, useState } from 'react'
import GlobalTable from '../../elements/GlobalTable'
import { useTranslation } from 'react-i18next'
import CreateSite from './Site/CreateSite'
import {
    useNavigate,
    useLocation
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import { listSiteCode } from '../../../actions/siteActions';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { toastUI } from '../../../util/util';
import { SITE_INFORMATION_CREATE_RESET, SITE_INFORMATION_CREATE_SUCCESS, SITE_INFORMATION_UPDATE_RESET } from '../../../contsants/siteConstants';

const Dashboard = () => {

    let location = useLocation();
    let history = useNavigate();
    const dispatch = useDispatch();
    const siteDetails = useSelector((state) => state.site);
    const { userSiteList, createNewSite, updateExistingSite } = siteDetails
    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin
    const [currentDiv, setCurrentDiv] = useState('site')
    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(false)
    let toasted = false

    let siteData = []

    const [showModal, setShowModal] = useState(false)

    const {t} = useTranslation()

    const createFunction = () => {
        if (showModal == true) {
            setShowModal(false)
        } else {
            setShowModal(true)
        }
    }

    const redirectToDetail = (row) => {
        history('/dashboard/siteDetail?id=' + row.original.id)
    }

    useEffect(() => {
        dispatch(listSiteCode(userInfo.id))
    }, [])

    useEffect(() => {
        if (createNewSite) {
            if (createNewSite.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(createNewSite, setIsLoading, "Site", "created.")
                if (resp) {
                    dispatch({ type: SITE_INFORMATION_CREATE_RESET })
                }
                toasted = true
            }
        }
    }, [createNewSite])

    useEffect(() => {
        if (updateExistingSite){
            if (updateExistingSite.hasOwnProperty('loading') && toasted == false) {
                let resp = toastUI(updateExistingSite, setIsLoading, "Site", "updated.")
                if (resp) {
                    dispatch({ type: SITE_INFORMATION_UPDATE_RESET })
                }
                toasted = true
            }
        }
    }, [updateExistingSite])

    useEffect(() => {
        if (userSiteList) {
            setIsLoading(userSiteList.loading)
            if (userSiteList.error) {
                toast.error(userSiteList.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (userSiteList.siteList){
                siteData = []
                let no = 1
                userSiteList.siteList.map((site) => {
                    let obj = {
                        sNo: no,
                        siteName: site.siteName,
                        enable: site.siteActive,
                        setTimePeriod: site.periodStart + " - " + site.periodEnd,
                        id: site.id
                    }
                    no++
                    siteData.push(obj)
                })
            }
            setData(siteData)
        }
    }, [userSiteList])

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
                        {t('Site Name')}
                    </a>
                ),
                accessor: 'siteName',
            },
            {
                Header: () => (
                    <a>
                        {t('Enabled')}
                    </a>
                ),
                accessor: 'enable',
            },
            {
                Header: () => (
                    <a>
                        {t('Set Time Period')}
                    </a>
                ),
                accessor: 'setTimePeriod',
            }
        ],
        []
    )

    return (
        <>
        {isLoading && <Loader />}
        <div>
            <ul className='flex ml-1.5 mb-1.5'>
                <li className={currentDiv == 'site' ? "current bg-cyan-500 text-white p-2 rounded-l border" : "bg-white text-black p-2 rounded-l border"} id='site'><a className='cursor-pointer' onClick={() => setCurrentDiv('site')}>{t('Site Detail.1')}</a></li>
                <li className={currentDiv == 'user' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='user'><a className='cursor-pointer' onClick={() => setCurrentDiv('user')}>{t('Online User')}</a></li>
            </ul>
        </div>
        { currentDiv == 'site' ?
        <GlobalTable columns={columns} data={data} 
            createText={t('Create Site.1')} createFunction={createFunction} 
            enableDetail={true} detailFunction={redirectToDetail} />
        :
        ""
        }
        {showModal ? (
            <>
                <CreateSite createFunction={createFunction} />
            </>
        ) : null }
        </>
    )
}

export default Dashboard