import {
    USER_LOGIN_FAIL,
    USER_LOGIN_REQUEST,
    USER_LOGIN_SUCCESS,
    USER_LOGOUT,
    USER_DETAILS_RESET,
    USER_REGISTER_FAIL,
    USER_REGISTER_REQUEST,
    USER_REGISTER_SUCCESS,
    USER_DETAILS_REQUEST,
    USER_DETAILS_SUCCESS,
    USER_DETAILS_FAIL,
    USER_UPDATE_PROFILE_REQUEST,
    USER_UPDATE_PROFILE_SUCCESS,
    USER_UPDATE_PROFILE_FAIL,
    USER_UPDATE_PASSWORD_REQUEST,
    USER_UPDATE_PASSWORD_SUCCESS,
    USER_UPDATE_PASSWORD_FAIL,
    USER_LIST_REQUEST,
    USER_LIST_SUCCESS,
    USER_LIST_FAIL,
    USER_CREATE_REQUEST,
    USER_CREATE_SUCCESS,
    USER_CREATE_FAIL,
    USER_FORGOT_PASSWORD_REQUEST,
    USER_FORGOT_PASSWORD_SUCCESS,
    USER_FORGOT_PASSWORD_FAIL,
    USER_RESET_PASSWORD_REQUEST,
    USER_RESET_PASSWORD_SUCCESS,
    USER_RESET_PASSWORD_FAIL
} from '../contsants/userConstants'
import axios from 'axios'
import jwtDecode from 'jwt-decode';
import { data } from 'autoprefixer';

const apiUrl = process.env.REACT_APP_BASE_URL;
const frontUrl = process.env.REACT_APP_FRONT_URL;

export const login = (email, password) => async (dispatch) => {
    try {
        dispatch({
            type: USER_LOGIN_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Account/login',
            {email, password},
            config
        )

        let decodedToken = jwtDecode(data.token)

        const objectData = {
            email : decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
            role : decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
            id : decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
            expiryDate: decodedToken.exp,
            token : data.token
        }

        dispatch({
            type: USER_LOGIN_SUCCESS,
            payload: objectData
        })

        localStorage.setItem('userInfo', JSON.stringify(objectData))
    } catch (error) {
        dispatch({
            type: USER_LOGIN_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const logout = () => (dispatch) => {
    localStorage.removeItem('userInfo');
    localStorage.removeItem('siteId');

    dispatch({ type: USER_LOGOUT })
    dispatch({ type: USER_DETAILS_RESET })
}

export const register = (email, password, confirmPassword) => async (dispatch) => {
    try {
        dispatch({
            type: USER_REGISTER_REQUEST
        })
        
        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Account/register',
            { email, password, confirmPassword},
            config
        )

        if (data.message == "Successfully Record Inserted") {
            dispatch({
                type: USER_REGISTER_SUCCESS,
                payload: "DONE"
            })
    
            dispatch(login(email, password))
        }

    } catch (error) {
        dispatch({
            type: USER_REGISTER_FAIL,
            payload:
                error.response && error.response.data.message
                    ? error.response.data.message
                    : error.message
        })
    }
}

export const getProfile = (userId) => async (dispatch) => {
    try {
        dispatch({
            type: USER_DETAILS_REQUEST
        })
        
        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Account/getProfile?id=' + userId,
            config
        )

        dispatch({
            type: USER_DETAILS_SUCCESS,
            payload: data
        })

    } catch (error) {
        dispatch({
            type: USER_DETAILS_FAIL,
            payload:
                error.response && error.response.data.message
                    ? error.response.data.message
                    : error.message
        })
    }
}

export const updateProfile = (params) => async (dispatch) => {
    try {
        dispatch({
            type: USER_UPDATE_PROFILE_REQUEST
        })
        
        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const { data } = await axios.put(
            apiUrl + 'api/Account/updateProfile?dob=' + params.dob,
            {
                userId: params.userId,
                familyName_Kana: params.kanaFirstName,
                givenName_Kana: params.kanaLastName,
                familyName_Chinese: params.kanjiFirstName,
                givenName_Chinese: params.kanjiLastName,
                familyName_Roman: params.companyName,
                givenName_Roman: params.companyAddress,
                gender: params.gender,
                dob: params.dob,
                country: params.country,
                mobileNumber: params.mobileNumber,
                emergencyContactNumber: params.emergencyContactNumber,
                postbox: params.postalCode,
                prefecture: params.prefecture,
                city: params.city,
                address: params.address,
                email: params.email,
                employeeId: params.employeeId
            },
            config
        )

        dispatch({
            type: USER_UPDATE_PROFILE_SUCCESS,
            payload: data
        })

    } catch (error) {
        dispatch({
            type: USER_UPDATE_PROFILE_FAIL,
            payload:
                error.response && error.response.data.message
                    ? error.response.data.message
                    : error.message
        })
    }
}

export const updatePassword = (id, oldPassword, newPassword, confirmPassword) => async (dispatch) => {
    try {
        dispatch({
            type: USER_UPDATE_PASSWORD_REQUEST
        })

        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Account/changePassword',
            { 
                id,
                oldPassword,
                newPassword,
                confirmPassword
            },
            config
        )

        dispatch({
            type: USER_UPDATE_PASSWORD_SUCCESS,
            payload: data
        })

    } catch (error) {
        dispatch({
            type: USER_UPDATE_PASSWORD_FAIL,
            payload:
                error.response && error.title
                    ? error.title
                    : error.message
        })
    }
}

export const getUserList = () => async (dispatch) => {
    try {
        dispatch({
            type: USER_LIST_REQUEST
        })

        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Users/getUserlist',
            config
        )

        dispatch({
            type: USER_LIST_SUCCESS,
            payload: data
        })

    } catch (error) {
        dispatch({
            type: USER_LIST_FAIL,
            payload:
                error.response && error.title
                    ? error.title
                    : error.message
        })
    }
}

export const createUser = (userName, companyName, phoneNumber, siteId, siteName) => async (dispatch) => {
    try {
        dispatch({
            type: USER_CREATE_REQUEST
        })

        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/SuperAdmin/createUser',
            {
                userName,
                companyName,
                phoneNumber,
                siteId,
                siteName
            },
            config
        )

        dispatch({
            type: USER_CREATE_SUCCESS,
            payload: data
        })

        dispatch(getUserList())
    } catch (error) {
        dispatch({
            type: USER_CREATE_FAIL,
            payload:
                error.response && error.title
                    ? error.title
                    : error.message
        })
    }
}

export const forgotPassword = (email) => async (dispatch) => {
    try {
        dispatch({
            type: USER_FORGOT_PASSWORD_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        let clientURI = frontUrl + 'resetPassword'

        const { data } = await axios.post(
            apiUrl + 'api/Account/forgotPassword',
            {email, clientURI},
            config
        )

        dispatch({
            type: USER_FORGOT_PASSWORD_SUCCESS,
            payload: data
        })

    } catch (error) {
        dispatch({
            type: USER_FORGOT_PASSWORD_FAIL,
            payload:
                error.response && error.response.data
                    ? error.response.data
                    : error.message
        })
    }
}

export const resetPasswordByToken = (email,password,confirmPassword,token) => async (dispatch) => {
    try {
        dispatch({
            type: USER_RESET_PASSWORD_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/Account/resetPassword',
            {email, password, confirmPassword, token},
            config
        )

        dispatch({
            type: USER_RESET_PASSWORD_SUCCESS,
            payload: data
        })

    } catch (error) {
        dispatch({
            type: USER_RESET_PASSWORD_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.response.data.errors[0] ?
                        error.response.data.errors[0] :
                        error.message
        })
    }
}
