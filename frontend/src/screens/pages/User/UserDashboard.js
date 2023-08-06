import React, { useEffect, useRef, useState } from 'react'
import * as biIcon from "react-icons/bi";
import { useTranslation } from 'react-i18next'
import { useDispatch, useSelector } from 'react-redux';
import { getSiteDetailsForUserByQR } from '../../../actions/siteActions';
import { SITE_DETAIL_BY_QR_RESET } from '../../../contsants/siteConstants';
import { toastUI, userBoardFileData } from '../../../util/util';
import { getUserBoard } from '../../../actions/userBoardAction';
import UserBoardDetail from '../../elements/UserBoardDetail';
import moment from 'moment/moment';
import UserBoardSafety from '../../elements/UserBoardSafety';
import { getMenuList } from '../../../actions/menuActions';
import UserBoardCard from '../../elements/UserBoardCard';

const UserDashboard = () => {

    const {t} = useTranslation()
    const dispatch = useDispatch();
    const [userBoardData, setUserBoardData] = useState(false)

    const siteDetails = useSelector((state) => state.site);
    const { qrDetail } = siteDetails
    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin
    const userBoardDetails = useSelector((state) => state.userBoard);
    const { board } = userBoardDetails
    const menuDetails = useSelector((state) => state.menu);
    const { menuList } = menuDetails

    const [isLoading, setIsLoading] = useState(false)

    const[siteName, setSiteName] = useState('')
    const[todayDate, setTodayDate] = useState('')
    const[timePeriod, setTimePeriod] = useState('')
    const[browseTime, setBrowseTime] = useState('')
    const[declarationTitle, setDeclarationTitle] = useState('')
    const[declarationDescription, setDeclarationDescription] = useState('')
    const[choureyOne, setChoureyOne] = useState('')
    const[choureyTwo, setChoureyTwo] = useState('')
    const[disaster, setDisater] = useState('')

    const [currentDiv, setCurrentDiv] = useState('choureyOne')

    var siteId = localStorage.getItem('siteId');

    const[choureyOneData, setChoureyOneData] = useState([])
    const[choureyTwoData, setChoureyTwoData] = useState([])
    const[disasterData, setDisaterData] = useState([])

    // const [scn, setIsOpen] = useState(false);

    const scanQr = () => {
        dispatch(getSiteDetailsForUserByQR("asd","asd","asd","asd"))
    }

    useEffect(() => {
        if (siteId != undefined) {
            setUserBoardData(true)
            dispatch(getUserBoard(siteId, userInfo.id))
            dispatch(getMenuList(siteId))
        }
    }, [siteId])

    useEffect(() => {
        if (qrDetail?.detail?.message == null) {
            if (qrDetail?.detail?.siteId != null) {
                setUserBoardData(true)
                localStorage.setItem('siteId', qrDetail?.detail?.siteId)
                dispatch(getUserBoard(qrDetail?.detail?.siteId, userInfo.id))
                dispatch(getMenuList(qrDetail?.detail?.siteId))
            }
        }
    }, [qrDetail])

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
            {userBoardData == true ?
                <div> 
                    <UserBoardDetail siteName={siteName} todayDate={todayDate} timePeriod={timePeriod} browseTime={browseTime} />
                    <UserBoardSafety declarationTitle={declarationTitle} declarationDescription={declarationDescription}/>
                    <div className='p-4'>
                        <ul className='flex'>
                            <li className={currentDiv == 'choureyOne' ? "current bg-cyan-500 text-white p-2 rounded-l border" : "bg-white text-black p-2 rounded-l border"} id='site'><a className='cursor-pointer' onClick={() => setCurrentDiv('choureyOne')}>{choureyOne}</a></li>
                            <li className={currentDiv == 'choureyTwo' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='user'><a className='cursor-pointer' onClick={() => setCurrentDiv('choureyTwo')}>{choureyTwo}</a></li>
                            <li className={currentDiv == 'disaster' ? "current bg-cyan-500 text-white p-2 rounded-r border" : "bg-white text-black p-2 rounded-r border"} id='user'><a className='cursor-pointer' onClick={() => setCurrentDiv('disaster')}>{disaster}</a></li>
                        </ul>
                    </div>
                    { currentDiv == 'choureyOne' ?
                        (
                            <UserBoardCard data={choureyOneData} type='C1' admin={false}/>
                        ) : currentDiv == 'choureyTwo' ?
                        (
                            <UserBoardCard data={choureyTwoData} type='C2' admin={false}/>
                        ) : (
                            <UserBoardCard data={disasterData} type='DS' admin={false}/>
                        )
                    }
                </div>
            :
                <div className='flex flex-col justify-center items-center'> 
                    Please scan to get further details
                    <biIcon.BiQrScan onClick={scanQr} className='text-9xl'/>
                </div>
            }
        </>
        
    )
}

export default UserDashboard