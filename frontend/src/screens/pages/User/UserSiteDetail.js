import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import DatePicker from 'react-datepicker'
import "react-datepicker/dist/react-datepicker.css";
import TimePicker from 'react-time-picker';
import 'react-time-picker/dist/TimePicker.css';
import {
    Link,
    useNavigate,
    useLocation
} from "react-router-dom";
import QRCode from 'react-qr-code'
import * as bsIcon from "react-icons/bs";
import { v4 as uuidv4 } from 'uuid';
import { FaCogs } from "react-icons/fa"
import { useDispatch, useSelector } from 'react-redux';
import { GoogleMap, LoadScript, MarkerF, useJsApiLoader } from "@react-google-maps/api";
import moment from 'moment';
import { createSite } from '../../../actions/siteActions';

const UserSiteDetail = () => {

    let history = useNavigate();
    const location = useLocation();
    const dispatch = useDispatch();

    const [siteName, setSiteName] = useState("");
    const [gpsRange, setGpsRange] = useState("");
    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());
    const [browseFrom, setBrowseFrom] = useState('');
    const [browseTo, setBrowseTo] = useState('');
    const userDetails = useSelector((state) => state.user);
    const siteDetails = useSelector((state) => state.site);
    const { userInfo } = userDetails
    const { siteValidation, siteDetail } = siteDetails

    const [markerPosition, setMarkerPosition] = useState('');

    const { isLoaded } = useJsApiLoader({
        id: 'google-map-script',
        googleMapsApiKey: 'AIzaSyBeHiA7IBPch3gqpcPpkzupx58eMqy7qNk',
        libraries: ['geometry', 'drawing'],
    });

    const [code, setCode] = useState('');   

    function generateUUID() {
        const uuid = uuidv4();
        setCode(uuid)
    }

    const {t} = useTranslation()

    useEffect(() => {
        generateUUID()
        setMarkerPosition({
            lat: 37.7749, 
            lng: -122.4194 
        })
    },[])

    const submitHandler = (e) => {
        e.preventDefault();
        let params = {
            siteName: siteName,  
            periodStart: startDate,
            qrCodeValue: code,
            periodEnd: endDate,
            browseTimeFrom: browseFrom,
            browseTimeTo: browseTo,
            gpsRange: gpsRange,
            siteAccessCode: siteValidation.siteCode,
            userId: userInfo.id,
            userName: userInfo.name,
            longitude: markerPosition.lng,
            latitude: markerPosition.lat
        }
        dispatch(createSite(params));
        history('/')
    }

    const handleMarkerDragEnd = (event) => {
        const { latLng } = event;
        const lat = latLng.lat();
        const lng = latLng.lng();
        setMarkerPosition({ lat, lng });
    };

    return (
        <div x-data="lw" class="flex flex-col justify-evenly">
            <div
                class="flex flex-col overflow-hidden bg-white rounded-md shadow-lg max "
            >
                <div class="flex-1 p-5 bg-white">
                    <div className='flex'>
                        <h2 className='flex ml-1 text-lg'>
                        {t('Site Detail.1')}
                        </h2>
                    </div>
                    
                    <div className='flex flex-col lg:flex-row justify-evenly'>
                        <form action="#" class="flex flex-auto flex-col space-y-5">
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="siteName" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Site Name')}:*</label>
                                    <input
                                        type="text"
                                        id="siteName"
                                        value={siteName}
                                        onChange={(e) => setSiteName(e.target.value)}
                                        autoFocus
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                            <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                                <div class="flex flex-1 flex-col p-1">
                                    <div class="flex items-center justify-between">
                                        <label for="dateOfBirth" class="text-sm font-semibold text-gray-500">{t('Period Start')}</label>
                                    </div>
                                    <DatePicker className='border flex-1 border-gray-300 rounded px-4 py-2 w-full' id="dateOfBirth" selected={startDate} onChange={(date) => {
                                        setStartDate(date)
                                        }} />
                                </div>
                                <div class="flex flex-1 flex-col p-1">
                                    <div class="flex items-center justify-between">
                                        <label for="dateOfBirth" class="text-sm font-semibold text-gray-500">{t('Period End')}</label>
                                    </div>
                                    <DatePicker className='border flex-1 border-gray-300 rounded px-4 py-2 w-full' id="dateOfBirth" selected={endDate} onChange={(date) => setEndDate(date)} />
                                </div>
                            </div>
                            <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="gpsRange" class="text-sm font-semibold text-gray-500 flex flex-start">{t('GPS Range(m)')}:*</label>
                                    <input
                                        type="text"
                                        id="gpsRange"
                                        value={gpsRange}
                                        onChange={(e) => setGpsRange(e.target.value)}
                                        autoFocus
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                                <div className='flex flex-1 flex-row p-1'>
                                    <div className='flex flex-1 flex-col p-1'>
                                        <label for="browseFrom" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Browse Time From')}:*</label> 
                                        <TimePicker
                                            className="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                            disableClock={true}
                                            value={browseFrom}
                                            onChange={setBrowseFrom}
                                        />
                                    </div>
                                    <div className='flex flex-1 flex-col p-1'>
                                        <label for="browseTo" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Browse Time To')}:*</label> 
                                        <TimePicker
                                            className="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                            disableClock={true}
                                            value={browseTo}
                                            onChange={setBrowseTo}
                                        />
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="longitude" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Longitude')}:*</label>
                                    <input
                                        type="text"
                                        id="longitude"
                                        disabled
                                        value={markerPosition.lng}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="latitude" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Latitude')}:*</label> 
                                    <input
                                        type="text"
                                        id="latitude"
                                        value={markerPosition.lat}
                                        disabled
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                        </form>
                        <div class="customizableQr flex flex-col flex-auto space-y-5 ml-8">
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="postal" class="text-sm font-semibold text-gray-500 flex flex-start">{t('QR Value')}:*</label>
                                    <QRCode
                                        size={225}
                                        bgColor="white"
                                        fgColor='black'
                                        value={code}
                                    />
                                </div>
                            </div>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex w-60 flex-col p-1'>
                                    <input
                                        type="text"
                                        id="postal"
                                        value={code}
                                        disabled
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex w-60 flex-col p-1'>
                                <button
                                    onClick={generateUUID}
                                    type="submit"
                                    class="w-full px-4 py-2 text-lg flex justify-center items-center font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                                >
                                    <FaCogs /> <span className='px-2'>{t('Generate')}</span>
                                </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className='flex flex-row mb-4'>
                    {isLoaded && <GoogleMap
                            mapContainerStyle={{ height: "100vh", width: "100%" }}
                            center={markerPosition}
                            zoom={12}
                        >
                            <MarkerF
                            position={markerPosition}
                            draggable={true}
                            onDragEnd={handleMarkerDragEnd}
                            />
                        </GoogleMap>
                    }
                    </div>
                    <div className='flex flex-row mt-2'>
                        <div className='mr-4'>
                            <button
                                onClick={submitHandler}
                                type="submit"
                                class="w-full px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                            >
                                <p>{t("Create Site.1")}</p>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default UserSiteDetail