import React from 'react'
import { useTranslation } from 'react-i18next';
import { Link, useLocation } from 'react-router-dom';
import * as antIcon from "react-icons/ai";
import * as remixIcon from  "react-icons/ri";
import * as bsIcon from "react-icons/bs";
import * as tfIcon from "react-icons/tfi"; 
import * as cgIcon from "react-icons/cg";
import * as hiIcon from "react-icons/hi"; 
import * as mdIcon from "react-icons/md";
import * as fiIcon from "react-icons/fi";
import { useEffect } from 'react';

const MenuItem = ({ item: { id, title, notifications, link }, onClick, selected }) => {
    let location = useLocation();

    const {t} = useTranslation()

    return (
        <Link to={'/' + link}>
            <div
            className={selected == link ? "w-full mt-6 flex items-center px-3 sm:px-0 xl:px-3 justify-start sm:justify-center xl:justify-start sm:mt-6 xl:mt-3 cursor-pointer sidebar-item-selected" : "w-full mt-6 flex items-center px-3 sm:px-0 xl:px-3 justify-start sm:justify-center xl:justify-start sm:mt-6 xl:mt-3 cursor-pointer sidebar-item"}
            onClick={() => onClick(link)}
            >  
            <SidebarIcons title={title} />
            {title == "Chourey 1" || title == "Chourey 2" || title == "Disaster" ?
                <div className="block sm:hidden xl:block ml-2">{title}</div>
            : 
                <div className="block sm:hidden xl:block ml-2">{t(title + '.1')}</div>
            }
            <div className="block sm:hidden xl:block flex-grow" />
            {notifications && (
                <div className="flex sm:hidden xl:flex bg-pink-600  w-5 h-5 flex items-center justify-center rounded-full mr-2">
                <div className="text-white text-sm">{notifications}</div>
                </div>
            )}
            </div>
        </Link>
    )
}

function SidebarIcons({ title }) {
    if (title == 'Dashboard') {
        return <antIcon.AiOutlineDashboard />
    } else if (title == 'User Board') {
        return <remixIcon.RiDashboardLine />
    } else if (title == 'Menu Name') {
        return <bsIcon.BsMenuButtonWideFill />
    } else if (title == 'Chourey 1') {
        return <tfIcon.TfiBlackboard />
    } else if (title == 'Chourey 2') {
        return <tfIcon.TfiBlackboard />
    } else if (title == 'Disaster') {
        return <cgIcon.CgDanger />
    } else if (title == 'Safety Declaration') {
        return <antIcon.AiOutlineSafety />
    } else if (title == 'Report') {
        return <hiIcon.HiOutlineDocumentReport />
    } else if (title == 'CHOUREY Print') {
        return <bsIcon.BsPrinter />
    } else if (title == 'Sub Admin') {
        return <remixIcon.RiUserSettingsLine />
    } else if (title == "Device Mapping") {
        return <bsIcon.BsPhone />
    } else if (title == "Sites Storage") {
        return <tfIcon.TfiHarddrive/>
    } else if (title == "Common Contents") {
        return <mdIcon.MdOutlineContentCopy/>
    } else if (title == "User List") {
        return <fiIcon.FiUsers/>
    } else if (title == "Create Site") {
        return <mdIcon.MdCreate />
    }
}

export default MenuItem