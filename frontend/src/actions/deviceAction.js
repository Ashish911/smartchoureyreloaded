import axios from 'axios'
import { ASSIGN_DEVICE_MAPPING_LIST_FAIL, ASSIGN_DEVICE_MAPPING_LIST_REQUEST, ASSIGN_DEVICE_MAPPING_LIST_SUCCESS, DELETE_DEVICE_MAPPING_LIST_FAIL, DELETE_DEVICE_MAPPING_LIST_REQUEST, DELETE_DEVICE_MAPPING_LIST_SUCCESS, GET_DEVICE_MAPPING_LIST_FAIL, GET_DEVICE_MAPPING_LIST_REQUEST, GET_DEVICE_MAPPING_LIST_SUCCESS, UPDATE_DEVICE_MAPPING_LIST_FAIL, UPDATE_DEVICE_MAPPING_LIST_REQUEST, UPDATE_DEVICE_MAPPING_LIST_SUCCESS } from '../contsants/deviceConstants';

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getDeviceList = () => async (dispatch) => {
    try {
        dispatch({
            type: GET_DEVICE_MAPPING_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/DeviceSite/getDeviceRegistrations',
            config
        ) 

        dispatch({
            type: GET_DEVICE_MAPPING_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_DEVICE_MAPPING_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateDeviceMapping = (params) => async (dispatch) => {
    try {
        dispatch({
            type: UPDATE_DEVICE_MAPPING_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.put(
            apiUrl + 'api/DeviceSite/updateDeviceRegistration',
            {
                deviceRegistrationId: params.deviceRegistrationId,
                newPhoneNumber: params.newPhoneNumber,
                oldPhoneNumber: params.oldPhoneNumber,
                deviceUniqueId: params.deviceUniqueId
            },
            config
        )

        dispatch({
            type: UPDATE_DEVICE_MAPPING_LIST_SUCCESS,
            payload: data
        })

        dispatch(getDeviceList())
    } catch (error) {
        dispatch({
            type: UPDATE_DEVICE_MAPPING_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const assignDeviceMapping = (params) => async (dispatch) => {
    try {
        dispatch({
            type: ASSIGN_DEVICE_MAPPING_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/DeviceSite/assignDeviceToSite',
            { 
                fkSiteId: params.fkSiteId,
                fkDeviceRegistrationId: params.fkDeviceRegistrationId
            },
            config
        ) 

        dispatch({
            type: ASSIGN_DEVICE_MAPPING_LIST_SUCCESS,
            payload: data
        })
        dispatch(getDeviceList())
    } catch (error) {
        dispatch({
            type: ASSIGN_DEVICE_MAPPING_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteDeviceMapping = (params) => async (dispatch) => {
    try {
        dispatch({
            type: DELETE_DEVICE_MAPPING_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/DeviceSite/deleteDeviceRegistration?deviceRegistrationId=' + params.id,
            config
        ) 

        dispatch({
            type: DELETE_DEVICE_MAPPING_LIST_SUCCESS,
            payload: data
        })

        dispatch(getDeviceList())
    } catch (error) {
        dispatch({
            type: DELETE_DEVICE_MAPPING_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}