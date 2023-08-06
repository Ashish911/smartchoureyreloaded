import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import Navbar from './elements/Navbar'
import {
    useLocation,
    useNavigate,
    Link
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import Sidebar from './elements/Sidebar';
import UserDashboard from './pages/User/UserDashboard';
import UserCreateSite from './pages/User/UserCreateSite';
import UserSiteDetail from './pages/User/UserSiteDetail';
import UserDisasterDetail from './pages/User/UserDisasterDetail';
import UserChoureyOneDetail from './pages/User/UserChoureyOneDetail';
import UserChoureyTwoDetail from './pages/User/UserChoureyTwoDetail';

const userDashboardItems = [
    [
        { id: '0', title: 'Dashboard', notifications: false, link: 'userDashboard'},
        { id: '1', title: 'Create Site', notifications: false, link: 'userDashboard/createSite'}
    ]
]


const UserBoardDashboardPage = ({ selectedLink, onchange, i18next }) => {
    const [selected, setSelected] = useState('userDashboard');
    const [showSidebar, onSetShowSidebar] = useState(false);

    const dispatch = useDispatch();
    const menuDetails = useSelector((state) => state.menu);
    const { menuList } = menuDetails

    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin
    let location = useLocation();
    let history = useNavigate();
    const redirect = location.search ? location.search.split('=')[1] : '/'

    useEffect(() => {
        if (selectedLink) {
            setSelected(selectedLink)
        }
    }, [selectedLink])

    useEffect(() => {

        if (userInfo) {
            if (userInfo.role == 'Admin' || userInfo.role == "SubAdmin") {
                history('/dashboard')
            } else if (userInfo.role == "Super Admin") {
                history('/adminDashboard')
            }
        } else {
            history(redirect)
        }
    }, [history, userInfo, redirect])

    return (
        <div className="flex">
            <Sidebar
                onSidebarHide={() => {
                    onSetShowSidebar(false);
                }}
                showSidebar={showSidebar}
                setSelected={setSelected}
                selected={selected}
                adminItems={userDashboardItems}
            />
            <UserContent
                onSidebarHide={() => {
                onSetShowSidebar(true);
                }}
                selected={selected}
                onchange={onchange}
                i18next={i18next}
            />
        </div>
    )
}

function UserContent({ onSidebarHide, selected, onchange, i18next }) {
    return (
        <div className="flex w-full">
            <div className="w-full h-screen hidden sm:block sm:w-20 xl:w-60 flex-shrink-0">.</div>
            <div className="flex flex-col flex-1 h-full overflow-hidden">
            <Navbar onchange={onchange} i18next={i18next} onSidebarHide={onSidebarHide} showDropdown={false} enableProfile={true}/>
            {/* <main className='flex-1 max-h-full p-5 overflow-hidden overflow-y-scroll'> */}
            <main className='flex-1 max-h-full p-5 overflow-hidden bg-gray-100'>
                <GetCurrentSelected selected={selected}/>
            </main>
            </div>
        </div>
    )
}

function GetCurrentSelected(selected) {
    if (selected.selected == 'userDashboard') {
        return <UserDashboard />
    } else if (selected.selected == 'userDashboard/createSite') {
        return <UserCreateSite />
    } else if (selected.selected == 'userDashboard/siteDetail') {
        return <UserSiteDetail />
    } else if (selected.selected == 'userDashboard/disasterDetail') {
        return <UserDisasterDetail />
    } else if (selected.selected == 'userDashboard/choureyOneDetail') {
        return <UserChoureyOneDetail />
    }  else if (selected.selected == 'userDashboard/choureyTwoDetail') {
        return <UserChoureyTwoDetail />
    }
}

export default UserBoardDashboardPage