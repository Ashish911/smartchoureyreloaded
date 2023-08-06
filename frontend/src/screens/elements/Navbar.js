import React, { useEffect, useRef, useState } from 'react'
import { useTranslation } from 'react-i18next'
import {
    Link,
    useNavigate
} from "react-router-dom";
import InitialsAvatar from 'react-initials-avatar';
import 'react-initials-avatar/lib/ReactInitialsAvatar.css';
import { useDispatch, useSelector } from 'react-redux';
import { getProfile, logout } from '../../actions/userActions';
import { getMenuList } from '../../actions/menuActions';
import { getSiteSpaceDetail } from '../../actions/siteActions';
import * as biIcon from "react-icons/bi";

const Navbar = ({onchange, i18next, showDropdown, setCurrentLang, onSidebarHide, enableProfile}) => {
    const [isOpen, setIsOpen] = useState(false);

    const {t} = useTranslation()

    const openDropdown = (() => {
        setIsOpen(!isOpen)
    })
    let history = useNavigate();

    const [userName, setUserName] = useState('User')

    const dispatch = useDispatch();
    const [selectedSite, setSelectedSite] = useState('')

    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin

    const userProfile = useSelector((state) => state.profile);
    const { detail } = userProfile

    const siteDetails = useSelector((state) => state.site);
    const { userSiteList } = siteDetails

    let menuRef = useRef()

    const selectSite = (value) => {
        setSelectedSite(value)
        localStorage.setItem('siteId', value)
    }

    useEffect(() => {
        let handler = e => {
            if (menuRef && menuRef.current != null) {
                if (!menuRef.current.contains(e.target)){
                    setIsOpen(false)
                }
            }
        }

        document.addEventListener("mousedown", handler)

        return() => {
            document.addEventListener("mousedown", handler)
        }
    },[i18next])

    let counter = 0

    useEffect(() => {
        if (detail?.userDetail != null) {
            if (Object.keys(detail?.userDetail).length === 0 && counter == 0) {
                if (userInfo != undefined) {
                    dispatch(getProfile(userInfo.id))
                    counter++
                }
            } else {
                if (detail?.userDetail?.familyName_Chinese !== undefined && detail?.userDetail?.givenName_Chinese != undefined) {
                    var name = detail.userDetail.familyName_Chinese.toString() + ' ' + detail.userDetail.givenName_Chinese.toString()
                    setUserName(name)
                }
            }
        }
    }, [detail?.userDetail])

    useEffect(() => {
        if (userSiteList.siteList) {
            if (userSiteList.siteList.length > 0) {
                if (selectedSite == '') {
                    if (localStorage.getItem('siteId') != undefined) {
                        setSelectedSite(localStorage.getItem('siteId'))
                    } else {
                        setSelectedSite(userSiteList.siteList[0].id)
                        localStorage.setItem('siteId', userSiteList.siteList[0].id)
                    }
                }
            }
        }
    }, [userSiteList])

    useEffect(() => {
        if (selectedSite != '') {
            document.cookie = "SiteId=" + selectedSite +"; path=/; expires=Fri, 31 Dec 2023 23:59:59 GMT; SameSite=None; Secure";
            document.cookie = "UserId=" + userInfo.id + "; path=/; expires=Fri, 31 Dec 2023 23:59:59 GMT; SameSite=None; Secure";
            dispatch(getMenuList(selectedSite))
            dispatch(getSiteSpaceDetail(selectedSite))
        }
    },[selectedSite])

    const logoutHandler = () => {
        dispatch(logout())
    }

    return (
        <header className="flex-shrink-0 border-b">
            <div className="flex items-center justify-between p-2">
                <div className="flex items-center space-x-3">
                    <span className="p-2 text-xl font-semibold tracking-wider uppercase lg:hidden">SC</span>
                    <button 
                    onClick={onSidebarHide}
                    className="p-2 rounded-md focus:outline-none focus:ring lg:hidden">
                        <biIcon.BiMenuAltRight className='text-xl' />
                    </button>   
                </div>

                {showDropdown == true ?
                    <div className='items-center px-2 space-x-2 md:flex-1 md:flex md:mr-auto md:ml-5'>
                    <div className="relative rounded-md border-gray-300 text-cyan-600 bg-slate-900 shadow-lg w-2/5">
                        <label for="frm-whatever" className="sr-only">My field</label>
                        <select className="appearance-none w-full py-1 px-2 bg-white" name="whatever" id="frm-whatever" onChange={(e) => selectSite(e.target.value)} value={selectedSite}>
                        {userSiteList.siteList && userSiteList.siteList.map((product) => (
                            <option value={product.id}>{product.siteName}</option>
                        ))}
                        </select>
                        <div className="pointer-events-none absolute right-0 top-0 bottom-0 flex items-center px-2 text-cyan-700 border-l ml-2">
                            <svg className="h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                                <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
                            </svg>
                        </div>
                    </div>
                </div>
                :
                ''
                }

                <div className="relative flex items-center space-x-3">
                    <div className='items-center space-x-3 md:flex'>
                        <div className="relative rounded-md border-gray-300 text-cyan-600 bg-white shadow-lg">
                        <label htmlFor="frm-whatever" className="sr-only">My field</label>
                        <select className="px-4 py-3 rounded-md hover:bg-gray-100 lg:max-w-sm md:py-2 md:flex-1 focus:outline-none md:focus:bg-gray-100 md:focus:shadow md:focus:border" 
                        name="whatever" id="frm-whatever" onChange={(e) => {
                            setCurrentLang(e)
                            onchange(e)
                        }} value={i18next.language}>
                            <option value="en-US">English</option>
                            <option value="ja">日本語</option>
                        </select>
                        </div>
                    </div>  
                    
                    <div className="relative" x-data="{ isOpen: false }" ref={menuRef}>
                        <button 
                        onClick={openDropdown}
                        className="p-1 bg-gray-200 rounded-full focus:outline-none focus:ring">
                        <InitialsAvatar className=" bg-cyan-500 text-white rounded-full p-2" name={userName}/>
                        {/* <img className="object-cover w-8 h-8 rounded-full" src="https://avatars0.githubusercontent.com/u/57622665?s=460&amp;u=8f581f4c4acd4c18c33a87b3e6476112325e8b38&amp;v=4" alt="Ahmed Kamel" /> */}
                        </button>
                        
                        {/* <div className="absolute right-0 p-1 bg-green-400 rounded-full bottom-3 animate-ping"></div>
                        <div className="absolute right-0 p-1 bg-green-400 border border-white rounded-full bottom-3"></div> */}

                        <div 
                        // @click.away="isOpen = false" 
                        className={isOpen ? "absolute mt-3 transform -translate-x-full bg-white rounded-md shadow-lg min-w-max transition ease-in" : "absolute mt-3 transform -translate-x-full bg-white rounded-md shadow-lg min-w-max hidden"}>
                        <div className="flex flex-col p-4 space-y-1 font-medium border-b">
                            <span className="text-gray-800">{userName ? userName : 'User'}</span>
                            <span className="text-sm text-gray-400">{userInfo?.email ? userInfo?.email : 'user@gmail.com'}</span>
                        </div>
                        <ul className="flex flex-col p-2 my-2 space-y-1">
                            {enableProfile == true ? 
                            <li>
                                <Link to={'/dashboard/profile'} className="block px-2 py-1 transition rounded-md hover:bg-gray-100">{t('Profile.1')}</Link>
                            </li>
                            : 
                                ''
                            }
                            <li onClick={logoutHandler}>
                                <p className="block px-2 py-1 transition rounded-md hover:bg-gray-100">{t('Logout.1')}</p>
                            </li>
                        </ul>
                        </div>
                    </div>
                </div>
            </div>
        </header>  
    )
}

export default Navbar