import React, { useEffect } from 'react'
import { useState } from 'react'
import { AiOutlineDelete } from 'react-icons/ai'

const PhotoEditDisplay = ({data, type, categoryId, setDeleteModal, setData}) => {

    const [photoData, setPhotoData] = useState([])
    var siteId = localStorage.getItem('siteId');
    var userInfo = localStorage.getItem('userInfo');

    let datas = []

    useEffect(() => {
        if (data) {
            datas = []
            let no = 1
            data.map((element) => {
                let obj = {
                    id: no,
                    url: element.url,
                    uniqueId: element.uniqueId
                }
                no++
                datas.push(obj)
            })
        }
        setPhotoData(datas)
    }, [data])

    const deleteFunction = (id, fileId) => {
        if (id != undefined) {
            setDeleteModal(true)
            let params = {
                fileId : fileId,
                siteId : siteId,
                categoryId : categoryId,
                category : 0,
                fileType : 1,
                type: 1,
                token: JSON.parse(userInfo).token
            }

            if (type == 'ChoureyOne') {
                params.category = 1
            } else if (type == 'ChoureyTwo') {
                params.category = 2
            } else {
                params.category = 3
            }
            setData(params)
        }
    }

    return (
        <div id='photoSection' className='flex'>
            {photoData?.map((item) => (
                <div id='onHover' class="w-[200px] sm:w-[250px] md:w-[300px] p-2 mr-2 relative duration-300 transition-all">
                    <img src={item.url} alt="" />
                    {/* <img src={`data:image/png;base64,${item.url}`} alt="Image" /> */}
                        <div id='none' class="absolute top-3 right-5 px-2 py-2 bg-red-500 rounded-full cursor-pointer" onClick={() => {
                            deleteFunction(item.id, item.uniqueId)
                        }}>
                            <AiOutlineDelete className='text-xl text-white'/>
                        </div>
                </div>
            ))}
        </div>
    )
}

export default PhotoEditDisplay