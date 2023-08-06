import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import Sidebar from './elements/Sidebar';
import Navbar from './elements/Navbar'
import AdminDashboard from './pages/SuperAdmin/AdminDashboard';
import DeviceMapping from './pages/SuperAdmin/DeviceMapping';
import SiteStorage from './pages/SuperAdmin/SiteStorage';
import CommonContent from './pages/SuperAdmin/CommonContent';
import Report from './pages/SuperAdmin/Report';
import UserList from './pages/SuperAdmin/UserList';
import {
    useLocation,
    useNavigate,
    Link
} from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import CreateCommonContent from './pages/SuperAdmin/Content/CreateCommonContent';

const superAdminItems = [
    [
        { id: '0', title: 'Dashboard', notifications: false, link: 'adminDashboard'},
        { id: '1', title: 'Device Mapping', notifications: false, link: 'adminDashboard/mapping'},
        { id: '2', title: 'Sites Storage', notifications: false, link: 'adminDashboard/siteStorage'},
        { id: '3', title: 'Common Contents', notifications: false, link: 'adminDashboard/commonContents'},
        { id: '4', title: 'Report', notifications: false, link: 'adminDashboard/report'},
        { id: '5', title: 'User List', notifications: false, link: 'adminDashboard/userList'}
    ]
]

const SuperAdminMainDashboardPage = ({ selectedLink, onchange, i18next }) => {
    const [selected, setSelected] = useState('adminDashboard');
    const [showSidebar, onSetShowSidebar] = useState(false);
    const [currentLang, setCurrentLang] = useState('')

    let location = useLocation();
    let history = useNavigate();

    const userLogin = useSelector((state) => state.user);
    const { userInfo } = userLogin
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
            } else if (userInfo.role == "User") {
                history('/userDashboard')
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
                adminItems={superAdminItems}
            />
            <AdminContent
                onSidebarHide={() => {
                onSetShowSidebar(true);
                }}
                selected={selected}
                onchange={onchange}
                i18next={i18next}
                setCurrentLang={setCurrentLang}
            />
        </div>
    )
}

function AdminContent({ onSidebarHide, selected, onchange, i18next, setCurrentLang }) {
    return (
        <div className="flex w-full">
            <div className="w-full h-screen hidden sm:block sm:w-20 xl:w-60 flex-shrink-0">.</div>
            <div className="flex flex-col flex-1 h-full overflow-hidden">
            <Navbar onchange={onchange} i18next={i18next} onSidebarHide={onSidebarHide} showDropdown={false} enableProfile={false} setCurrentLang={setCurrentLang}/>
            {/* <main className='flex-1 max-h-full p-5 overflow-hidden overflow-y-scroll'> */}
            <main className='flex-1 max-h-full p-5 overflow-hidden bg-gray-100'>
                <GetCurrentSelected selected={selected}/>
            </main>
            </div>
        </div>
    )
}

function GetCurrentSelected(selected) {
    if (selected.selected == 'adminDashboard') {
        return <AdminDashboard />
    } else if (selected.selected == 'adminDashboard/mapping') {
        return <DeviceMapping />
    } else if (selected.selected == 'adminDashboard/siteStorage') {
        return <SiteStorage />
    } else if (selected.selected == 'adminDashboard/commonContents') {
        return <CommonContent />
    } else if (selected.selected == 'adminDashboard/report') {
        return <Report />
    } else if (selected.selected == 'adminDashboard/userList') {
        return <UserList />
    } else if (selected.selected == 'adminDashboard/createCommonContents') {
        return <CreateCommonContent isEdit={false} />
    } else if (selected.selected == 'adminDashboard/commonContentDetails') {
        return <CreateCommonContent isEdit={true} />
    }
}

export default SuperAdminMainDashboardPage