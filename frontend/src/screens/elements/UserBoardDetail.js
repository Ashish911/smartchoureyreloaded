import React from 'react'
import { useTranslation } from 'react-i18next'
import * as aiIcon from "react-icons/ai";
import * as bsIcon from "react-icons/bs";
import * as tiIcon from "react-icons/ti";
import * as tbIcon from "react-icons/tb";

const UserBoardDetail = ({siteName, todayDate, timePeriod, browseTime}) => {

    const {t} = useTranslation()

    return (
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
                                        <span class="text-gray-400">SITE NAME</span>
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
                                        <span class="text-gray-400">TODAY'S DATE</span>
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
                                        <span class="text-gray-400">TIME PERIOD</span>
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
                                        <span class="text-gray-400">BROWSE TIME</span>
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

export default UserBoardDetail