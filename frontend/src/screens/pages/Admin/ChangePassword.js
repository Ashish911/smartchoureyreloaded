import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import i18next from 'i18next'
import { useDispatch, useSelector } from 'react-redux';
import { updatePassword } from '../../../actions/userActions';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { USER_UPDATE_PASSWORD_RESET } from '../../../contsants/userConstants';
import { toastUI } from '../../../util/util';

const ChangePassword = () => {
    
    const {t} = useTranslation()
    const dispatch = useDispatch();

    const [email, setEmail] = useState('');
    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const userDetails = useSelector((state) => state.user);
    const { userInfo } = userDetails

    const userProfile = useSelector((state) => state.profile);
    const { passwordUpdate } = userProfile

    const [isLoading, setIsLoading] = useState(false)

    useEffect(() => {
        if (userInfo != undefined) {
            setEmail(userInfo.email)
        }
    }, [userInfo])

    const submitHandler = (e) => {
        if ( validate(oldPassword) && validate(newPassword, confirmPassword)) {
            dispatch(updatePassword(userInfo.id, oldPassword, newPassword, confirmPassword))
        }
    }

    const resetState = () => {
        setOldPassword('')
        setNewPassword('')
        setConfirmPassword('')
    }

    const validate = (password, confirmPassword = null) => {

        if (!newPassword) {
            toast.error('Password is required !!', {
                position: toast.POSITION.TOP_RIGHT
            })
            return false
        }

        if (password.length < 8) {
            toast.error('Password must be at least 8 characters', {
                position: toast.POSITION.TOP_RIGHT
            })
            return false
        }
    
        if (!/[A-Z]/.test(password)) {
            toast.error('Password should contain at least one uppercase letter.', {
                position: toast.POSITION.TOP_RIGHT
            })
            return false
        }
    
        if (!/[a-z]/.test(password)) {
            toast.error('Password should contain at least one lowercase letter.', {
                position: toast.POSITION.TOP_RIGHT
            })
            return false
        }
    
        if (!/[0-9]/.test(password)) {
            toast.error('Password should contain at least one digit.', {
                position: toast.POSITION.TOP_RIGHT
            })
            return false
        }
    
        if (!/[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/.test(password)) {
            toast.error('Password should contain at least one special character.', {
                position: toast.POSITION.TOP_RIGHT
            })
            return false
        }

        if (confirmPassword != null) {
            if (password !== confirmPassword) {
                toast.error('Confirm password is not matched', {
                    position: toast.POSITION.TOP_RIGHT
                })
                return false
            }
        }

        return true
    }

    useEffect(() => {
        if (passwordUpdate.success == true) {
            let resp = toastUI(passwordUpdate, setIsLoading, "Password", "updated.")
            if (resp == true) {
                resetState()
            } 
            dispatch({ type: USER_UPDATE_PASSWORD_RESET })
        }
    }, [passwordUpdate])

    return (
        <div x-data="lw" class="flex h-full justify-evenly items-center">
            <div
                class="flex flex-col sm:w-1/2 overflow-hidden bg-white rounded-md shadow-lg max "
            >
                {isLoading && <Loader />}
                <div class="flex-1 p-5 bg-white">
                <form action="#" class="flex flex-col space-y-5">
                    <div class="flex flex-row space-y-1 items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="emailAddress" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Email')}:*</label>
                            <input
                                type="email"
                                id="emailAddress"
                                value={email}
                                disabled
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-row space-y-1 items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="oldPassword" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Old Password')}:*</label>
                            <input
                                type="password"
                                id="oldPassword"
                                value={oldPassword}
                                onChange={(e) => setOldPassword(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-row space-y-1 items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="newPassword" class="text-sm font-semibold text-gray-500 flex flex-start">{t('New Password')}:*</label>
                            <input
                                type="password"
                                id="newPassword"
                                value={newPassword}
                                onChange={(e) => setNewPassword(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-row space-y-1 items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="confirmPassword" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Confirm Password.1')}:*</label>
                            <input
                                type="password"
                                id="confirmPassword"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div>
                    <button
                        onClick={submitHandler}
                        type="button"
                        class="px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                    >
                        {t('Change Password')}
                    </button>
                    </div>
                </form>
                </div>
            </div>
        </div>
    )
}

export default ChangePassword