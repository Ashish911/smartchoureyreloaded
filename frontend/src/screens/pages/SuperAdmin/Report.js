import React, { useMemo, useState, useEffect } from 'react'
import GlobalTable from '../../elements/GlobalTable'
import { useTranslation } from 'react-i18next'
import ReportSelect from '../../elements/ReportSelect'
import { useDispatch, useSelector } from 'react-redux';
import { getAdminReportList, getDeviceReportList, getLoggedUserReportList, getSubAdminReportList, getUserAccessReportList } from '../../../actions/reportAction';
import moment from 'moment'
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { getColumns, manipulateReportData } from '../../../util/util'

const Report = () => {

    const dispatch = useDispatch();

    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());
    const [reportType, setReportType] = useState('ad');
    const [siteName, setSiteName] = useState('')

    const siteDetail = useSelector((state) => state.site);
    const { userSiteList } = siteDetail

    const [collectData, setCollectData] = useState(false)

    const [data, setData] = useState([]);
    const [columns, setColumns] = useState([]);
    const [isLoading, setIsLoading] = useState(false)

    const reportInfo = useSelector((state) => state.report);

    const {adminReportList, subAdminReportList, 
        loggedUserReportList, deviceReportList } = reportInfo 

    var reportData = []

    var siteId = localStorage.getItem('siteId');

    const handleSubmit = () => {
        let fromDate = moment(startDate, 'ddd MMM DD YYYY HH:mm:ss [GMT]ZZ').format("YYYY/MM/DD")
        let toDate = moment(endDate, 'ddd MMM DD YYYY HH:mm:ss [GMT]ZZ').format("YYYY/MM/DD")
        let obj = {
            "sNo": "S.No"
        }

        if (reportType == 'ad') {
            dispatch(getAdminReportList(siteName))
            obj.sitename = t('Site Name')
            obj.kananame = t('Full Name (Kana)')
            obj.kanjiname = t('Full Name (Kanji)')
            obj.email = t('Email')
            obj.setperiod = t('Set Time Period')
            obj.dateofregister = t('Date Of Register')
        } else if (reportType == "sba") {
            dispatch(getSubAdminReportList(siteName))
            obj.sitename = t('Site Name')
            obj.kananame = t('Full Name (Kana)')
            obj.kanjiname = t('Full Name (Kanji)')
            obj.email = t('Email')
            obj.setperiod = t('Set Time Period')
            obj.dateofregister = t('Date Of Register')
        } else if (reportType == "liul") {
            let site = userSiteList.siteList.find(obj => obj.siteName.includes(siteName))
            dispatch(getLoggedUserReportList(site.id))
            obj.sitename = t('Site Name')
            obj.kananame = t('Full Name (Kana)')
            obj.kanjiname = t('Full Name (Kanji)')
            obj.email = t('Email')
            obj.setperiod = t('Set Time Period')
            obj.dateofregister = t('Date Of Register')
        } else if (reportType == "dl") {
            dispatch(getDeviceReportList(siteId, 
                fromDate , 
                toDate))
            obj.phoneno = t('Phone Number')
            obj.browsedate = t('Browse Date')
            obj.sitename = t('Site Name')
        }
        getColumns(setColumns, obj)
        setCollectData(true)
    }

    useEffect(() => {
        let list
        if (reportType == 'ad') {
            list = adminReportList
        } else if (reportType == 'sba') {
            list = subAdminReportList
        } else if (reportType == 'liul') {
            list = loggedUserReportList
        } else if (reportType == 'dl') {
            list = deviceReportList
        } 
        if (list) {
            setIsLoading(list.loading)
            if (list.error) {
                toast.error(list.error, {
                    position: toast.POSITION.TOP_RIGHT
                })
            }
            if (list.list) {
                setData(manipulateReportData(reportType, list, reportData))
            }
        }
    }, [adminReportList, subAdminReportList, 
        loggedUserReportList, deviceReportList])

    const {t} = useTranslation()

    return (
        <>
            {isLoading && <Loader />}
            <ReportSelect startDate={startDate} setStartDate={setStartDate} 
            endDate={endDate} setEndDate={setEndDate} handleSubmit={handleSubmit}
            reportType={reportType} setSiteName={setSiteName} setReportType={setReportType} isSuperAdmin={true}/>
            {collectData ? 
            (<GlobalTable columns={columns} data={data} sheetName={true} />)
            :
            ''
            }
        </>
    )
}

export default Report