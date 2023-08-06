import React from 'react'
import DatePicker from 'react-datepicker'
import { useTranslation } from 'react-i18next'

const ReportSelect = ({startDate, setStartDate, endDate, setEndDate, handleSubmit, reportType, setReportType, isSuperAdmin, siteName, setSiteName}) => {
    const {t} = useTranslation()

    return(
        <div className={reportType == "ad" || reportType == "sba" || reportType == "liul" 
            ? "flex flex-col sm:flex-row md:items-center justify-start p-2 mb-4" :
            "flex flex-col md:flex-row md:items-center justify-start p-2 mb-4" }>
            <div className="relative flex items-center space-x-3 pr-2">
                <div className='items-center space-x-3 md:flex'>
                    <div className="relative rounded-md border-gray-300 text-cyan-600 bg-white shadow-lg">
                    <label htmlFor="frm-whatever" className="sr-only">My field</label>
                    <select className="px-4 py-3 rounded-md hover:bg-gray-100 lg:max-w-sm md:py-2 md:flex-1 focus:outline-none md:focus:bg-gray-100 md:focus:shadow md:focus:border" 
                    name="whatever" id="frm-whatever"
                    value={reportType}
                    onChange={event => setReportType(event.target.value)}>
                        {isSuperAdmin ? 
                            <>
                                <option value="ad">{t('Admin')}</option>
                                <option value="sba">{t('Sub Admin.1')}</option>
                                <option value="liul">{t('Logged in User List')}</option>
                                <option value="dl">{t('Device List')}</option>
                            </>
                        :
                            <>
                                <option value="ua">{t('User Access')}</option>
                                <option value="sd">{t('Safety Declaration.1')}</option>
                                <option value="sua">{t('Smartphone: User Access')}</option>
                                <option value="spsd">{t('SP: Safety Declaration')}</option>
                                <option value="susp">{t('Site User SmartPhone')}</option>
                                <option value="ol">{t('Operation Log')}</option>
                            </>
                        }
                        
                    </select>
                    </div>
                </div>  
            </div>
            {reportType == "ad" || reportType == "sba" || reportType == "liul" ?
                <>
                    <div className="relative flex items-center space-x-3 pr-2">
                        <div className='items-center space-x-3 md:flex'>
                            <div className="relative rounded-md border-gray-300 text-cyan-600 bg-white shadow-lg">
                                <input
                                    type="text"
                                    id="siteName"
                                    value={siteName}
                                    placeholder='Site Name'
                                    onChange={(e) => setSiteName(e.target.value)}
                                    autoFocus
                                    class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                />
                            </div>
                        </div>  
                    </div>
                </>
            :
                <div className='flex flex-col sm:flex-row py-2 md:p-0'>
                    <div className="relative flex items-center space-x-3 pr-2 py-1 sm:py-0">
                        <div className='items-center space-x-3 md:flex'>
                            <div className="relative rounded-md border-gray-300 text-cyan-600 bg-white shadow-lg">
                                <DatePicker 
                                yearDropdown
                                showYearDropdown
                                scrollableYearDropdown
                                showMonthDropdown
                                className='border flex-1 border-gray-300 rounded px-4 py-2 w-full' id="dateOfBirth" selected={startDate} onChange={(date) => setStartDate(date)} />
                            </div>
                        </div>  
                    </div>
                    <div className="relative flex items-center space-x-3 pr-2">
                        <div className='items-center space-x-3 md:flex'>
                            <div className="relative rounded-md border-gray-300 text-cyan-600 bg-white shadow-lg">
                                <DatePicker
                                yearDropdown
                                showYearDropdown
                                scrollableYearDropdown
                                showMonthDropdown 
                                className='border flex-1 border-gray-300 rounded px-4 py-2 w-full' id="dateOfBirth" selected={endDate} onChange={(date) => setEndDate(date)} />
                            </div>
                        </div>  
                    </div>
                </div>
            }
            <a
            className="inline-flex items-center justify-center px-4 py-2 space-x-1 
            bg-cyan-600 text-white cursor-pointer rounded-md shadow hover:bg-opacity-20"
            onClick={handleSubmit}>
                <span>{t('Search')}</span>
            </a>
        </div>
    )
}

export default ReportSelect