import React, { useEffect, useState } from 'react'
import * as antIcon from "react-icons/ai";
import { useTranslation } from 'react-i18next'
import { useDispatch, useSelector } from 'react-redux';
import { getUserBoard } from '../../../actions/userBoardAction';
import moment from 'moment/moment';
import * as aiIcon from "react-icons/ai";
import * as bsIcon from "react-icons/bs";
import * as tiIcon from "react-icons/ti";
import * as tbIcon from "react-icons/tb";
import {
    useNavigate
} from "react-router-dom";
import { userBoardFileData } from '../../../util/util';

function Detail({siteName, todayDate, timePeriod, browseTime}) {

    const {t} = useTranslation()

    const dispatch = useDispatch();

    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin
    var siteId = localStorage.getItem('siteId');

    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getUserBoard(siteId, userInfo.id))
        }
    },[siteId])

    return(
        <div className="shadow-lg rounded m-4 sm:flex p-4 bg-white">
            <div className='flex flex-col w-full'>
                <div className='flex justify-start'>
                    <h1 className='p-2 font-extrabold text-xl'>{t('Site Detail.1')}</h1>
                </div>
                <div className='p-2 flex flex-col sm:flex-row justify-evenly'>
                    <div className='flex flex-col lg:flex-row justify-evenly'>
                        <div className='flex-1 p-1'>
                            <div class="p-4 transition-shadow border rounded-lg shadow-sm hover:shadow-lg bg-neutral-50 bg-opacity-80">
                                <div class="flex items-start justify-evenly">
                                    <div class="sm:p-0 md:p-3 lg:p-6 rounded-md">
                                        <tiIcon.TiInfo className='text-4xl text-cyan-400'/>
                                    </div>
                                    <div class="flex flex-col space-y-2 text-start">
                                        <span class="text-gray-400">{t('Site Name')}</span>
                                        <span class="md:text-sm lg:text-md mt-0 lg:font-semibold">{siteName}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className='flex-1 p-1'>
                            <div class="p-4 transition-shadow border rounded-lg shadow-sm hover:shadow-lg bg-neutral-50 bg-opacity-80">
                                <div class="flex items-start justify-evenly">
                                    <div class="sm:p-0 md:p-3 lg:p-6 rounded-md">
                                        <bsIcon.BsCalendar2Date className='text-4xl text-cyan-400'/>
                                    </div>
                                    <div class="flex flex-col space-y-2 text-start">
                                        <span class="text-gray-400">{t('Todays Date')}</span>
                                        <span class="md:text-sm lg:text-md mt-0 lg:font-semibold">{todayDate}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className='flex flex-col lg:flex-row justify-evenly'>
                        <div className='flex-1 p-1'>
                            <div class="p-4 transition-shadow border rounded-lg shadow-sm hover:shadow-lg bg-neutral-50 bg-opacity-80">
                                <div class="flex items-start justify-evenly">
                                    <div class="pr-3 sm:p-0 md:p-3 lg:p-6 rounded-md">
                                        <tbIcon.TbCalendarTime className='text-4xl text-cyan-400'/>
                                    </div>
                                    <div class="flex flex-col space-y-2 text-start">
                                        <span class="text-gray-400">{t('Time Period')}</span>
                                        <span class="md:text-sm lg:text-md mt-0 lg:font-semibold">{timePeriod}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className='flex-1 p-1'>
                            <div class="p-4 transition-shadow border rounded-lg shadow-sm hover:shadow-lg bg-neutral-50 bg-opacity-80">
                                <div class="flex items-start justify-evenly">
                                    <div class="pr-3 sm:p-0 md:p-3 lg:p-6 rounded-md">
                                        <aiIcon.AiOutlineFieldTime className='text-4xl text-cyan-400'/>
                                    </div>
                                    <div class="flex flex-col space-y-2 text-start">
                                        <span class="text-gray-400">{t('Browse Time')}</span>
                                        <span class="md:text-sm lg:text-md mt-0 lg:font-semibold">{browseTime}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

function Safety({declarationTitle, declarationDescription}){

    return(
        <div className="shadow-lg rounded m-4 sm:flex p-4 bg-white">
            <div className='flex flex-col w-full'>
                <div className='flex justify-start'>
                    <h1 className='p-2 font-extrabold text-xl'>{declarationTitle}</h1>
                </div>
                <div className='p-2 flex flex-col justify-start'>
                    <div className='flex items-center h-10 px-2 rounded hover:bg-gray-100'>
                        <span class="ml-4 text-sm">{declarationDescription}</span>
                    </div>
                </div>
            </div>
        </div>
    )
}

function Card ({ data, type }){

    let history = useNavigate();

    var selectedId = null

    const editFunction = () => {
        if (type == 'C1') {
            history('/dashboard/createChoureyOneEdit?id=' + selectedId)
        } else if (type == 'C2') {
            history('/dashboard/createChoureyTwoEdit?id=' + selectedId)
        } else {
            history('/dashboard/editDisaster?id=' + selectedId)
        }
    }

    const detailFunction = () => {
        if (type == 'C1') {
            history('/dashboard/choureyOneDetail?id=' + selectedId)
        } else if (type == 'C2') {
            history('/dashboard/choureyTwoDetail?id=' + selectedId)
        } else {
            history('/dashboard/disasterDetail?id=' + selectedId)
        }
    }

    return (
        <div className='flex flex-wrap p-4 ml-2'>
            {data?.map(item => {
                return (
                    <div class="flex justify-center my-4">
                        <div class="w-80 p-5 rounded-md shadow-xl bg-white">
                            {item?.url ? <img src={item?.url} className='max-h-52 w-full' alt="Image"/> : 'No Image'}
                            <div className='flex justify-between'>
                                <h2 class="text-md font-bold mt-3">{item?.title}</h2>
                                <div className='flex justify-center text-xl items-center mt-3'>
                                    <antIcon.AiOutlineEdit className='text-cyan-500 cursor-pointer' onClick={() => {
                                            selectedId = item?.id
                                            editFunction()
                                        }}/>
                                    <antIcon.AiOutlineInfoCircle className='text-cyan-500 cursor-pointer' onClick={() => {
                                            selectedId = item?.id
                                            detailFunction()
                                        }}/>
                                </div>
                            </div>
                            <p class="flex justify-start text-gray-400 text-sm mb-2 overflow-x-hidden">{item?.description}.</p>
                        </div>
                    </div>
                )
            })}
        </div>
        
    )
}

const UserBoard = () => {
    
    const dispatch = useDispatch();

    const {t} = useTranslation()

    var siteId = localStorage.getItem('siteId');
    const[siteName, setSiteName] = useState('')
    const[todayDate, setTodayDate] = useState('')
    const[timePeriod, setTimePeriod] = useState('')
    const[browseTime, setBrowseTime] = useState('')
    const[declarationTitle, setDeclarationTitle] = useState('')
    const[declarationDescription, setDeclarationDescription] = useState('')
    const[choureyOne, setChoureyOne] = useState('')
    const[choureyTwo, setChoureyTwo] = useState('')
    const[disaster, setDisater] = useState('')

    const[choureyOneData, setChoureyOneData] = useState([])
    const[choureyTwoData, setChoureyTwoData] = useState([])
    const[disasterData, setDisaterData] = useState([])

    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin

    const userBoardDetails = useSelector((state) => state.userBoard);
    const { board } = userBoardDetails

    const menuDetails = useSelector((state) => state.menu);
    const { menuList } = menuDetails

    const [currentDiv, setCurrentDiv] = useState('choureyOne')

    useEffect(() => {
        if (siteId != undefined) {
            dispatch(getUserBoard(siteId, userInfo.id))
        }
    },[siteId])

    useEffect(() => {
        if (board != undefined) {
            let date = new Date()
            setTodayDate(moment(date, 'ddd MMM DD YYYY HH:mm:ss [GMT]ZZ').format("DD/MM/YYYY"))
            setSiteName(board?.board?.siteName)
            setTimePeriod(board?.board?.projectTimeLine)
            setBrowseTime(board?.board?.siteTime)
            setDeclarationTitle(board?.board?.siteDeclarationTitle)
            setDeclarationDescription(board?.board?.siteDeclarationDescription)
            setChoureyOne(menuList?.menuList?.chourey1)
            setChoureyTwo(menuList?.menuList?.chourey2)
            setDisater(menuList?.menuList?.disaster)

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
            <Detail siteName={siteName} todayDate={todayDate} timePeriod={timePeriod} browseTime={browseTime} />
            <Safety declarationTitle={declarationTitle} declarationDescription={declarationDescription}/>
            <div className='p-4'>
                <ul className='flex'>
                    <li className={currentDiv == 'choureyOne' ? "current bg-cyan-500 text-white p-2 rounded-l border" : "bg-white text-black p-2 rounded-l border"} id='site'><a className='cursor-pointer' onClick={() => setCurrentDiv('choureyOne')}>{choureyOne}</a></li>
                    <li className={currentDiv == 'choureyTwo' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='user'><a className='cursor-pointer' onClick={() => setCurrentDiv('choureyTwo')}>{choureyTwo}</a></li>
                    <li className={currentDiv == 'disaster' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='user'><a className='cursor-pointer' onClick={() => setCurrentDiv('disaster')}>{disaster}</a></li>
                </ul>
            </div>
            { currentDiv == 'choureyOne' ?
                (
                    <Card data={choureyOneData} type='C1'/>
                ) : currentDiv == 'choureyTwo' ?
                (
                    <Card data={choureyTwoData} type='C2'/>
                ) : (
                    <Card data={disasterData} type='DS'/>
                )
            }
        </>
    )
}

export default UserBoard