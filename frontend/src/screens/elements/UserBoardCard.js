import React from 'react'
import {
    useNavigate
} from "react-router-dom";
import * as antIcon from "react-icons/ai";

const UserBoardCard = ({ data, type, admin }) => {

    let history = useNavigate();

    var selectedId = null

    var api = admin == true ? "/dashboard" : "/userDashboard"

    const editFunction = () => {
        if (type == 'C1') {
            history(api + '/createChoureyOneEdit?id=' + selectedId)
        } else if (type == 'C2') {
            history(api + '/createChoureyTwoEdit?id=' + selectedId)
        } else {
            history(api + '/editDisaster?id=' + selectedId)
        }
    }

    const detailFunction = () => {
        if (type == 'C1') {
            history(api + '/choureyOneDetail?id=' + selectedId)
        } else if (type == 'C2') {
            history(api + '/choureyTwoDetail?id=' + selectedId)
        } else {
            history(api + '/disasterDetail?id=' + selectedId)
        }
    }

    return (
        <div className='flex flex-wrap p-4 ml-2'>
            {data?.map(item => {
                return (
                    <div class="flex justify-start my-4 ml-4">
                        <div class="w-80 p-5 rounded-md shadow-xl bg-white">
                        {item?.url ? <img src={item?.url} className='max-h-52 w-full' alt="Image"/> : 'No Image'}
                            <div className='flex justify-between'>
                                <h2 class="text-md font-bold mt-3">{item?.title}</h2>
                                <div className='flex justify-center text-xl items-center mt-3'>
                                    {admin == true ? 
                                        <antIcon.AiOutlineEdit className='text-cyan-500 cursor-pointer' onClick={() => {
                                            selectedId = item?.id
                                            editFunction()
                                        }}/>
                                    :
                                        ''
                                    }
                                    
                                    <antIcon.AiOutlineInfoCircle className='text-cyan-500 cursor-pointer' onClick={() => {
                                            selectedId = item?.id
                                            detailFunction()
                                        }}/>
                                </div>
                            </div>
                            <p class="flex justify-start text-gray-400 text-sm mb-2">{item.description}.</p>
                        </div>
                    </div>
                )
            })}
        </div>
    )
}

export default UserBoardCard