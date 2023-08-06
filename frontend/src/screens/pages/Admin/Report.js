import React, { useMemo, useState, useEffect } from 'react'
import DatePicker from 'react-datepicker'
import GlobalTable from '../../elements/GlobalTable'
import { useTranslation } from 'react-i18next'
import ReportSelect from '../../elements/ReportSelect'
import { useDispatch, useSelector } from 'react-redux';
import { getOperationReportList, getSafetyDeclarationReportList, getSiteUserSmartPhoneReportList, getSmartPhoneUserAccessReportList, getSpSafetyDeclarationReportList, getUserAccessReportList } from '../../../actions/reportAction';
import moment from 'moment'
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { getColumns, manipulateReportData } from '../../../util/util'

const Report = () => {
    
    const dispatch = useDispatch();

    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());
    const [reportType, setReportType] = useState('ua');

    const [collectData, setCollectData] = useState(false)

    const [data, setData] = useState([]);
    const [columns, setColumns] = useState([]);
    const [isLoading, setIsLoading] = useState(false)

    const reportInfo = useSelector((state) => state.report);

    const {userReportList, safetyDeclarationReportList, 
        smartPhoneUserReportList, spSafetyDeclarationReportList, 
        operationReportList, siteUserSmartPhoneReportList } = reportInfo 

    var reportData = []
    
    var siteId = localStorage.getItem('siteId');

    const handleSubmit = () => {
        let fromDate = moment(startDate, 'ddd MMM DD YYYY HH:mm:ss [GMT]ZZ').format("YYYY/MM/DD")
        let toDate = moment(endDate, 'ddd MMM DD YYYY HH:mm:ss [GMT]ZZ').format("YYYY/MM/DD")
        let obj = {
            "sNo": "S.No"
        }

        if (reportType == 'ua') {
            dispatch(getUserAccessReportList(siteId, 
                fromDate , 
                toDate))
            obj.fullName = t('Full Name')
            obj.kanaName = t('KanaName')
            obj.email = t('Email')
            obj.browseDate = t('Browse Date')
            obj.browseTime = t('Browse Time')
            obj.companyName = t('Company Name')
        } else if (reportType == 'sd') {
            dispatch(getSafetyDeclarationReportList(siteId, 
                fromDate , 
                toDate))
            obj.fullName = t('Full Name')
            obj.kanaName = t('KanaName')
            obj.email = t('Email')
            obj.browseDate = t('Browse Date')
            obj.browseTime = t('Browse Time')
            obj.status = t('Status')
            obj.companyName = t('Company Name')
        } else if (reportType == 'sua') {
            dispatch(getSmartPhoneUserAccessReportList(siteId, 
                fromDate , 
                toDate))
            obj.phoneNumber = t('Phone Number')
            obj.browseDate = t('Browse Date')
            obj.browseTime = t('Browse Time')
            obj.siteName = t('Site Name')
        } else if (reportType == "susp") {
            dispatch(getSiteUserSmartPhoneReportList(siteId, 
                fromDate , 
                toDate))
            obj.username = t('Username')
            obj.companyName = t('Company Name')
            obj.phoneNumber = t('Phone Number')
            obj.siteName = t('Site Name')
        } else if (reportType == 'spsd') {
            dispatch(getSpSafetyDeclarationReportList(siteId,
                fromDate,
                toDate))
            obj.siteName = t('Site Name')
            obj.phoneNumber = t('Phone Number')
            obj.deviceType = t('Device Type')
            obj.browseDate = t('Browse Date')
            obj.browseTime = t('Browse Time')
        } else if (reportType == 'ol') {
            dispatch(getOperationReportList(siteId, 
                fromDate , 
                toDate))
            obj.category = t('Category')
            obj.changeType = t('Change Type')
            obj.changeCategory = t('Change Category')
            obj.changedProperty = t('Changed Property')
            obj.email = t('Email')
            obj.browseDate = t('Browse Date')
            obj.browseTime = t('Browse Time')
        }
        getColumns(setColumns, obj)
        setCollectData(true)
    }

    useEffect(() => {
        let list
        if (reportType == 'ua') {
            list = userReportList
        } else if (reportType == 'sd') {
            list = safetyDeclarationReportList
        } else if (reportType == 'sua') {
            list = smartPhoneUserReportList
        } else if (reportType == 'spsd') {
            list = spSafetyDeclarationReportList
        } else if (reportType == 'ol') {
            list = operationReportList
        } else if (reportType == 'susp') {
            list = siteUserSmartPhoneReportList
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
    }, [userReportList, safetyDeclarationReportList, 
        smartPhoneUserReportList, spSafetyDeclarationReportList, 
        operationReportList, siteUserSmartPhoneReportList])

    const {t} = useTranslation()

    return (
        <>
            {isLoading && <Loader />}
            <ReportSelect startDate={startDate} setStartDate={setStartDate} 
            endDate={endDate} setEndDate={setEndDate} handleSubmit={handleSubmit}
            reportType={reportType} setReportType={setReportType} isSuperAdmin={false}/>
            {collectData ? 
            (<GlobalTable columns={columns} data={data} sheetName={true} />)
            :
            ''
            }
        </>
    )
}

export default Report