import React, { useState, useEffect } from 'react'
import { useTranslation } from 'react-i18next'
import GlobalTable from '../../elements/GlobalTable'
import ConfirmationDialog from '../../elements/ConfirmationDialog'
import {
    useNavigate,
} from "react-router-dom";
import SiteEdit from './SiteEdit';
import { useDispatch, useSelector } from 'react-redux';
import { getAllAggregateSiteSpaces } from '../../../actions/siteActions';
import { toastUI } from '../../../util/util';
import { ASSIGN_SITE_SPACE_RESET } from '../../../contsants/siteConstants';
import Loader from '../../elements/Loader';

const SiteStorage = () => {

    const dispatch = useDispatch();
    const siteDetails = useSelector((state) => state.site);
    const { sitesSpace, assignSiteSpace } = siteDetails
    const {t} = useTranslation()
    const [showModal, setShowModal] = useState(false)
    const [data, setData] = useState([]);
    const [selectedData, setSelectedData] = useState({})
    const [isLoading, setIsLoading] = useState(false)

    const editFunction = (row) => {
        if (showModal == true) {
            setSelectedData({})
            setShowModal(false)
        } else {
            setSelectedData({
                siteId: row.original.siteId,
                siteName: row.original.siteName,
                siteSpace: row.original.allocatedSpace
            })
            setShowModal(true)
        }
    }

    useEffect(() => {
        dispatch(getAllAggregateSiteSpaces());
    }, [])

    useEffect(() => {
        if (sitesSpace.siteSpace) {
            setData(manipulateData(sitesSpace.siteSpace))
        }
    },[sitesSpace])

    useEffect(() => {
        if (assignSiteSpace) {
            let resp = toastUI(assignSiteSpace, setIsLoading, "Site storage", "assigned.")
            if (resp) {
                dispatch({ type: ASSIGN_SITE_SPACE_RESET })
            }
        }
    }, [assignSiteSpace])

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
                        {t('Current Storage')}
                    </a>
                ),
                accessor: 'currentStorage',
            }
        ],
        []
    )

    const manipulateData = (code) => {
        let siteData = []
            let no = 1
            code.map((site) => {
                let obj = {
                    sNo: no,
                    siteName: site.siteName,
                    currentStorage: site.allocatedSpace / 1000 + " GB",
                    siteId: site.siteId,
                    allocatedSpace: site.allocatedSpace
                }
                no++
                siteData.push(obj)
        })
        return siteData
    }

    return (
        <>
            {isLoading && <Loader />}
            <GlobalTable columns={columns} data={data} 
                enableEdit={true} editFunction={editFunction} />
            {showModal ? (
                <>
                    <SiteEdit editFunction={editFunction} selectedData={selectedData}/>
                </>
            ) : null }
        </>
    )
}

export default SiteStorage