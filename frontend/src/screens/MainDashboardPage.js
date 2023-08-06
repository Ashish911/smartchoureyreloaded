import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import Navbar from './elements/Navbar'
import {
  useLocation,
  useNavigate,
  Link
} from "react-router-dom";
import Profile from './pages/Admin/Profile'
import ChangePassword from './pages/Admin/ChangePassword'
import UserBoard from './pages/Admin/UserBoard'
import * as antIcon from "react-icons/ai";
import * as remixIcon from  "react-icons/ri";
import * as bsIcon from "react-icons/bs";
import * as tfIcon from "react-icons/tfi"; 
import * as cgIcon from "react-icons/cg";
import * as hiIcon from "react-icons/hi"; 
import i18next from 'i18next';
import Report from './pages/Admin/Report';
import SubAdmin from './pages/Admin/SubAdmin';
import Dashboard from './pages/Admin/Dashboard';
import SiteDetail from './pages/Admin/Site/SiteDetail';
import ChoureyOne from './pages/Admin/ChoureyOne';
import ChoureyTwo from './pages/Admin/ChoureyTwo';
import SafetyDeclaration from './pages/Admin/SafetyDeclaration';
import Disaster from './pages/Admin/Disaster';
import CreateDisaster from './pages/Admin/Disaster/CreateDisaster';
import CreateChoureyOne from './pages/Admin/Chourey/CreateChoureyOne';
import CreateChoureyTwo from './pages/Admin/Chourey/CreateChoureyTwo';
import Menu from './pages/Admin/Menu';
import Print from './pages/Admin/Print';
import { useDispatch, useSelector } from 'react-redux';
import ProgressBar from "@ramonak/react-progress-bar";
import ChoureyOneDetail from './pages/Admin/Chourey/ChoureyOneDetail';
import DisasterDetail from './pages/Admin/Disaster/DisasterDetail';
import ChoureyTwoDetail from './pages/Admin/Chourey/ChoureyTwoDetail';

const MainDashboardPage = ({ selectedLink, onchange, i18next}) => {
  const [selected, setSelected] = useState('dashboard');
  const [showSidebar, onSetShowSidebar] = useState(false);

  const dispatch = useDispatch();
  const menuDetails = useSelector((state) => state.menu);
  const { menuList } = menuDetails
  let repitition = 0
  const [menuItems, setMenuItems] = useState([])

  let items = [
    { id: '0', title: 'Dashboard', notifications: false, link: 'dashboard'},
    { id: '1', title: 'User Board', notifications: false, link: 'dashboard/userboard'},
    { id: '2', title: 'Menu Name', notifications: false, link: 'dashboard/menu'},
    { id: '3', title: 'Chourey 1' , notifications: false, link: 'dashboard/choureyOne'},
    { id: '4', title: 'Chourey 2', notifications: false, link: 'dashboard/choureyTwo'},
    { id: '5', title: 'Disaster', notifications: false, link: 'dashboard/disaster'},
    { id: '6', title: 'Safety Declaration', notifications: false, link: 'dashboard/safetyDeclaration'},
    { id: '7', title: 'Report', notifications: false, link: 'dashboard/report'},
    { id: '8', title: 'CHOUREY Print', notifications: false, link: 'dashboard/print'}
  ]

  let location = useLocation();
  let history = useNavigate();

  const userLogin = useSelector((state) => state.user);
  const { userInfo } = userLogin
  const redirect = location.search ? location.search.split('=')[1] : '/'
  const [currentLang, setCurrentLang] = useState('')

  useEffect(() => {
    if (selectedLink) {
      setSelected(selectedLink)
    }
  }, [selectedLink])

  useEffect(() => {
    if (userInfo) {
      if (userInfo.role == "SuperAdmin") {
        history('/adminDashboard')
      } else if (userInfo.role == "User") {
        history('/userDashboard')
      } else {
        if (userInfo.role == "Admin"){
          if (repitition == 0) {
            let subAdminItem = {id: '9', title: 'Sub Admin', notifications: false, link: 'dashboard/subAdmin'}
            items.push(subAdminItem)
            repitition++
          }
        }
        setMenuItems(items)
      }
    } else {
      history('/')
    }
  }, [history, userInfo, redirect])

  useEffect(() => {
    if (menuList.menuList) {
      if (menuItems.length > 0) {
        changeMenu()
      }
    }
  }, [menuList, currentLang, selectedLink])


  function changeMenu() {
    if (i18next.language == 'en' || i18next.language == "en-US") {
      let updatedItems = menuItems.map(item => {
        if (item.id == 3) {
          item.title = menuList.menuList.chourey1
        } else if (item.id == 4) {
          item.title = menuList.menuList.chourey2
        } else if (item.id == 5) {
          item.title = menuList.menuList.disaster
        }
        return item
      })
      setMenuItems(updatedItems)
    } else {
      let updatedItems = menuItems.map(item => {
        if (item.id == 3) {
          item.title = menuList.menuList.chourey1Japanese
        } else if (item.id == 4) {
          item.title = menuList.menuList.chourey2Japanese
        } else if (item.id == 5) {
          item.title = menuList.menuList.disasterJapanese
        }
        return item
      })
      setMenuItems(updatedItems)
    }
  }

  return (
    <div className="flex">
      <Sidebar
        onSidebarHide={() => {
          onSetShowSidebar(false);
        }}
        showSidebar={showSidebar}
        setSelected={setSelected}
        selected={selected}
        items={menuItems}
      />
      <Content
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

function Sidebar({ onSidebarHide, showSidebar, setSelected, selected, items }) {
  
  const [allocated, setAllocated] = useState(0)
  const [used, setUsed] = useState(0)
  const [progress, setProgress] = useState(50);
  const siteDetails = useSelector((state) => state.site);
  const { siteSpaceDetail } = siteDetails

  useEffect(() => {
    if (siteSpaceDetail.detail) {
      setAllocated(siteSpaceDetail.detail.allocatedSpace)
      setUsed(siteSpaceDetail.detail.usedSpaceInMb)
      setProgress((siteSpaceDetail.detail.usedSpaceInMb/siteSpaceDetail.detail.allocatedSpace) * 100)
    }
  }, [siteSpaceDetail])

  return (
    <div className= {showSidebar ? "fixed inset-y-0 left-0 bg-card w-full sm:w-20 xl:w-60 sm:flex flex-col z-10 flex" : "fixed inset-y-0 left-0 bg-card w-full sm:w-20 xl:w-60 sm:flex flex-col z-10 hidden" }>
      <div className="flex-shrink-0 overflow-hidden p-2">
        <div className="flex items-center h-full sm:justify-center xl:justify-start p-2 sidebar-separator-top">
          <IconButton icon="res-react-dash-logo" className="w-10 h-10" />
          <div className="block sm:hidden xl:block ml-2 font-bold text-xl text-cyan-500">
            Smart Chourey
          </div>
          <div className="flex-grow sm:hidden xl:block" />
          <IconButton
            icon="res-react-dash-sidebar-close"
            className="block sm:hidden"
            onClick={onSidebarHide}
          />
        </div>
      </div>
      <div className="flex-grow overflow-x-hidden overflow-y-auto flex flex-col">
        {items.map((i) => (
          <MenuItem
            key={i.id}
            item={i}
            onClick={setSelected}
            selected={selected}
          />
        ))}
        <div className="w-full p-3 h-28 hidden sm:block sm:h-20 xl:h-32">
          <div
            className="rounded-xl w-full h-full px-3 sm:px-0 xl:px-3 overflow-hidden border-2 border-cyan-500"
          >
            <div className="block sm:hidden xl:block pt-3">
              <div className="font-bold text-gray-300 text-sm">Site Storage</div>
              <div className="text-gray-500 text-xs">
                Allocated Space : {allocated} MB
              </div>
              <div className="text-gray-500 text-xs">
                Used Space : {used} MB
              </div>
              <ProgressBar completed={progress} />
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

function MenuItem({ item: { id, title, notifications, link }, onClick, selected }) {

  let location = useLocation();
  const redirect = location.search ? location.search.split('=')[1] : '/'

  const {t} = useTranslation()

  return (                        
      <Link to={'/' + link}>
        <div
          className={selected == link ? "w-full mt-6 flex items-center px-3 sm:px-0 xl:px-3 justify-start sm:justify-center xl:justify-start sm:mt-6 xl:mt-3 cursor-pointer sidebar-item-selected" : "w-full mt-6 flex items-center px-3 sm:px-0 xl:px-3 justify-start sm:justify-center xl:justify-start sm:mt-6 xl:mt-3 cursor-pointer sidebar-item"}
          onClick={() => {
            onClick(link)
          }}
        >  
          <SidebarIcons title={title} id={id} />
          {id == 3 || id == 4 || id == 5 ?
            <div className="block sm:hidden xl:block ml-2">{title}</div>
          : 
            <div className="block sm:hidden xl:block ml-2">{t(title + '.1')}</div>
          }
          {/* <div className="block sm:hidden xl:block ml-2">{t(title + '.1')}</div> */}
          <div className="block sm:hidden xl:block flex-grow" />
          {notifications && (
            <div className="flex sm:hidden xl:flex bg-pink-600  w-5 h-5 flex items-center justify-center rounded-full mr-2">
              <div className="text-white text-sm">{notifications}</div>
            </div>
          )}
        </div>
      </Link>

  );
}

function GetCurrentSelected(selected) {
  if (selected.selected == 'dashboard') {
    return <Dashboard />
  } else if (selected.selected == 'dashboard/userboard') {
    return <UserBoard />
  } else if (selected.selected == 'dashboard/menu') {
    return <Menu />
  } else if (selected.selected == 'dashboard/choureyOne') {
    return <ChoureyOne />
  } else if (selected.selected == 'dashboard/choureyTwo') {
    return <ChoureyTwo />
  } else if (selected.selected == 'dashboard/disaster') {
    return <Disaster />
  } else if (selected.selected == 'dashboard/safetyDeclaration') {
    return <SafetyDeclaration />
  } else if (selected.selected == 'dashboard/report') {
    return <Report />
  } else if (selected.selected == 'dashboard/print') {
    return <Print />
  } else if (selected.selected == 'dashboard/subAdmin') {
    return <SubAdmin />
  } else if (selected.selected == 'dashboard/profile') {
    return <Profile />
  } else if (selected.selected == 'dashboard/changePassword') {
    return <ChangePassword />
  } else if (selected.selected == 'dashboard/createSite') {
    return <SiteDetail isDetail={false}/>
  } else if (selected.selected == 'dashboard/siteDetail') {
    return <SiteDetail isDetail={true}/>
  } else if (selected.selected == 'dashboard/createDisaster') {
    return <CreateDisaster isEdit={false} />
  } else if (selected.selected == 'dashboard/editDisaster') {
    return <CreateDisaster isEdit={true}/>
  } else if (selected.selected == 'dashboard/disasterDetail') {
    return <DisasterDetail />
  } else if (selected.selected == 'dashboard/createChoureyOne') {
    return <CreateChoureyOne isEdit={false}/>
  } else if (selected.selected == 'dashboard/createChoureyOneEdit') {
    return <CreateChoureyOne isEdit={true} />
  } else if (selected.selected == 'dashboard/choureyOneDetail') {
    return <ChoureyOneDetail />
  } else if (selected.selected == 'dashboard/createChoureyTwo') {
    return <CreateChoureyTwo isEdit={false} />
  } else if (selected.selected == 'dashboard/createChoureyTwoEdit') {
    return <CreateChoureyTwo isEdit={true} />
  } else if (selected.selected == 'dashboard/choureyTwoDetail') {
    return <ChoureyTwoDetail />
  }
}

function Content({ onSidebarHide, selected, onchange, i18next, setCurrentLang }) {

  return (
    <div className="flex w-full">
      <div className="w-full h-screen hidden sm:block sm:w-20 xl:w-60 flex-shrink-0">.</div>
      <div className="flex flex-col flex-1 h-full overflow-hidden">
        <Navbar onchange={onchange} i18next={i18next} onSidebarHide={onSidebarHide} showDropdown={true} enableProfile={true} setCurrentLang={setCurrentLang}/>
        {/* <main className='flex-1 max-h-full p-5 overflow-hidden overflow-y-scroll'> */}
        <main className='flex-1 max-h-full p-5 overflow-hidden bg-gray-100'>
          <GetCurrentSelected selected={selected}/>
        </main>
      </div>
    </div>
  )
}

function SidebarIcons({ title, id }) {
  if (title == 'Dashboard') {
    return <antIcon.AiOutlineDashboard />
  } else if (title == 'User Board') {
    return <remixIcon.RiDashboardLine />
  } else if (title == 'Menu Name') {
    return <bsIcon.BsMenuButtonWideFill />
  } else if (title == 'Chourey 1' || id == 3 ) {
    return <tfIcon.TfiBlackboard />
  } else if (title == 'Chourey 2' || id == 4) {
    return <tfIcon.TfiBlackboard />
  } else if (title == 'Disaster' || id == 5) {
    return <cgIcon.CgDanger />
  } else if (title == 'Safety Declaration') {
    return <antIcon.AiOutlineSafety />
  } else if (title == 'Report') {
    return <hiIcon.HiOutlineDocumentReport />
  } else if (title == 'CHOUREY Print') {
    return <bsIcon.BsPrinter />
  } else if (title == 'Sub Admin') {
    return <remixIcon.RiUserSettingsLine />
  } 
}

function IconButton({
  onClick = () => {},
  icon = 'options',
  className = 'w-4 h-4',
}) {
  return (
    <button onClick={onClick} type="button" className={className}>
      <img
        src={`https://assets.codepen.io/3685267/${icon}.svg`}
        alt=""
        className="w-full h-full"
      />
    </button>
  );
}

export default MainDashboardPage