import React, { useEffect, useState } from 'react'
import * as aiIcon from "react-icons/ai";
import { useDispatch, useSelector } from 'react-redux';
import { getSiteDetail } from '../../../actions/siteActions';
import { useTranslation } from 'react-i18next'
import { getUserBoard } from '../../../actions/userBoardAction';
import moment from 'moment';
import { userBoardFileData } from '../../../util/util';

const EntryDetail = () => {

    const {t} = useTranslation()

    const dispatch = useDispatch();
    var siteId = localStorage.getItem('siteId');
    
    const [siteName, setSiteName] = useState('')
    const [siteDate, setSiteDate] = useState('')

    const currentDate = new Date();

    let date = new Date()
    const day = moment(date, 'ddd MMM DD YYYY HH:mm:ss [GMT]ZZ').format("DD/MM/YYYY")

    // const day = currentDate.getDate();
    // const month = currentDate.getMonth() + 1;
    // const year = currentDate.getFullYear();

    const siteInfo = useSelector((state) => state.site);
    const { siteDetail } = siteInfo

    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getSiteDetail(siteId))
        }
    },[siteId])

    useEffect(() => {
        if (siteDetail) {
            if (siteDetail.siteInfo) {
                setSiteName(siteDetail.siteInfo.siteName)
                setSiteDate(siteDetail.siteInfo.periodStart + ' - ' + siteDetail.siteInfo.periodEnd)
            }
        }
    }, [siteDetail])


    const printHandler = () => {
        let divId = "printScreen"
        var printContents = document.getElementById(divId).cloneNode(true);
        var printContainer = document.createElement('div');
        printContainer.appendChild(printContents);
    
        var iframe = document.createElement('iframe');
        iframe.style.display = 'none';
        document.body.appendChild(iframe);
    
        var iframeDocument = iframe.contentWindow.document;
        iframeDocument.body.appendChild(printContainer);
    
        var link = iframeDocument.createElement('link');
        link.href = 'path/to/your/external-styles.css';
        link.rel = 'stylesheet';
        iframeDocument.head.appendChild(link);
    
        link.onload = function () {
            iframe.contentWindow.print();
            document.body.removeChild(iframe);
        };
    }

    return(
        <div className="shadow-lg rounded m-4 sm:flex p-4 bg-white">
            <div className='flex flex-col w-full'>
                <div className='flex justify-between'>
                    <h1 className='p-2 font-extrabold text-xl'>{siteName}</h1>
                    <h1 className='p-2 font-semibold text-xl'>{day}</h1>
                </div>
                <div className='flex justify-between'>
                    <h1 className='p-2 font-semibold text-xl'>{siteDate}</h1>
                    <button
                        onClick={printHandler}
                        class="px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                    >
                        {t('Print Chourey')}
                    </button>
                </div>
                <VideoSection />
            </div>
        </div>
    )
}

const VideoSection = () => {

    const {t} = useTranslation()

    return(
        <div className="shadow-lg rounded m-4 sm:flex p-4 bg-white">
            <div className='flex flex-col w-full'>
                <div className='flex justify-start'>
                    <h1 className='p-2 font-extrabold text-xl text-cyan-500'>Video</h1>
                </div>
                <div className='flex justify-start'>
                    <h1 className='p-2 font-semibold text-xl'>Please check the Smart Chourey Apps for the Video</h1>
                </div>
            </div>
        </div>
    )
}

const Card = ({data}) => {
    return(
        <>
            {data?.map(item => {
                return (
                    <div className='flex p-4 ml-2'>
                        <div class="flex flex-1 my-4">
                            <div class="p-5 w-full rounded-md shadow-xl bg-white">
                                <div className='flex justify-between p-4 bg-gray-50 rounded-md '>
                                    <h2 class="text-lg font-bold text-cyan-400">{item?.title}</h2>
                                </div>
                                {item?.url ? <img src={item?.url} className='max-h-52' alt="Image"/> : 'No Image'}
                                
                                <p class="flex justify-start text-sm mb-2 overflow-x-hidden">{item?.description}.</p>
                            </div>
                        </div>
                    </div>
                )
            })}
        </>
    )
}



const Print = () => {

    const dispatch = useDispatch();

    const {t} = useTranslation()

    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin

    const userBoardDetails = useSelector((state) => state.userBoard);
    const { board } = userBoardDetails

    var siteId = localStorage.getItem('siteId');

    const[choureyOneData, setChoureyOneData] = useState([])
    const[choureyTwoData, setChoureyTwoData] = useState([])
    const[disasterData, setDisaterData] = useState([])

    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getUserBoard(siteId, userInfo.id))
        }
    },[siteId])

    useEffect(() => {
        if (board != undefined) {

            if(board?.board?.choureyOneModelList.length > 0) {
                let data = manipulateData(board?.board?.choureyOneModelList, 'ChoureyOne')
                userBoardFileData(data, userInfo,setChoureyOneData)
            }

            if (board?.board?.choureyTwoModelList.length > 0) {
                let data = manipulateData(board?.board?.choureyTwoModelList, 'ChoureyTwo')
                userBoardFileData(data, userInfo,setChoureyTwoData)
            }

            if (board?.board?.disasterModelList.length > 0) {
                let data = manipulateData(board?.board?.disasterModelList, 'Disaster')
                userBoardFileData(data, userInfo,setDisaterData)
            }

        }
    }, [board])

    function manipulateData(data, type) {
        let newData = []
        if (data){
            let no = 1
            data.map((item) => {
                let obj = {
                    sNo: no,
                    title: item.title,
                    description: item.description,
                    id: type == "ChoureyOne" ? item.choureyOneId 
                    : type == "ChoureyTwo" ? item.choureyTwoId : item.disasterId,
                    image: item.photoListModel[0]
                }

                no++
                newData.push(obj)
            })
        }
        return newData
    }

    return (
        <>
            <div id='printScreen' className='printScreen'>
                <EntryDetail />
                <div className="shadow-lg rounded m-4 sm:flex p-4 bg-white flex-col">
                    <Card data={choureyOneData} />
                    <Card data={choureyTwoData} />
                    <Card data={disasterData} />
                </div>
            </div>
        </>
    )
}

export default Print