import React, { css } from 'react'
import { BarLoader } from 'react-spinners';

const Loader = () => {
    return (
        <div className='loader'>
            <div className="animate-spin rounded-full border-t-4 border-blue-500 border-solid border-t-blue-300 border-r-transparent border-b-transparent border-l-transparent h-12 w-12"></div>
            {/* <BarLoader color="#ffffff" loading={true} /> */}
        </div>
    )
}

export default Loader