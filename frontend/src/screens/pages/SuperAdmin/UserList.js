import React, { useState, useEffect } from 'react'
import { useTranslation } from 'react-i18next'
import GlobalTable from '../../elements/GlobalTable'
import CreateUser from './CreateUser'
import { useDispatch, useSelector } from 'react-redux';
import { getUserList } from '../../../actions/userActions';
import { USER_CREATE_RESET } from '../../../contsants/userConstants';
import { toastUI } from '../../../util/util';
import Loader from '../../elements/Loader';

const UserList = () => {

    const dispatch = useDispatch();
    const userCreateDetail = useSelector((state) => state.userCreate);
    const userDetail = useSelector((state) => state.userList);
    const { users } = userDetail
    const [showModal, setShowModal] = useState(false)
    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(false)

    const {t} = useTranslation()

    const createFunction = () => {
        if (showModal == true) {
            setShowModal(false)
        } else {
            setShowModal(true)
        }
    }

    useEffect(() => {
        dispatch(getUserList());
    }, [])

    useEffect(() => {
        if (userCreateDetail) {
            let resp = toastUI(userCreateDetail, setIsLoading, "User ", "created.")
            if (resp) {
                dispatch({ type: USER_CREATE_RESET })
            }
        }
    }, [userCreateDetail])

    useEffect(() => {
        if (users) {
            let userData = []
            let no = 1
            users.map((user) => {
                let obj = {
                    sNo: no,
                    userName: user.userName,
                    companyName: user.companyName,
                    phoneNo: user.phoneNumber,
                    siteName: user.siteName
                }
                no++
                userData.push(obj)
            })
            setData(userData)
        }
    },[users])

    const columns = React.useMemo(
        () => [
            {
                Header: () => (
                    <a>
                        {t('S.NO')}
                    </a>
                ),
                accessor: 'sNo', 
            },
            {
                Header: () => (
                    <a>
                        {t('Username')}
                    </a>
                ),
                accessor: 'userName',
            },
            {
                Header: () => (
                    <a>
                        {t('Company Name')}
                    </a>
                ),
                accessor: 'companyName',
            },
            {
                Header: () => (
                    <a>
                        {t('Phone Number')}
                    </a>
                ),
                accessor: 'phoneNo',
            },
            {
                Header: () => (
                    <a>
                        {t('Site Name')}
                    </a>
                ),
                accessor: 'siteName',
            }
        ],
        []
    )

    return (
        <>
            {isLoading && <Loader />}
            <GlobalTable columns={columns} data={data} 
                createText={t('Create User')} createFunction={createFunction} 
                enableDetail={false} enableDelete={false} /> 
            {showModal ? (
            <>
                <CreateUser createFunction={createFunction}/>
            </>
            ) : null }
        </>
    )
}

export default UserList