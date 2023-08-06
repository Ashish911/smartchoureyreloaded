import React from 'react'

const UserBoardSafety = ({declarationTitle, declarationDescription}) => {
    
    return (
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

export default UserBoardSafety