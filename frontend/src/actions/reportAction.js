import axios from 'axios'
import { GET_ADMIN_REPORT_LIST_FAIL, GET_ADMIN_REPORT_LIST_REQUEST, GET_ADMIN_REPORT_LIST_SUCCESS, GET_DEVICE_REPORT_LIST_FAIL, GET_DEVICE_REPORT_LIST_REQUEST, GET_DEVICE_REPORT_LIST_SUCCESS, GET_LOGGED_USER_REPORT_LIST_FAIL, GET_LOGGED_USER_REPORT_LIST_REQUEST, GET_LOGGED_USER_REPORT_LIST_SUCCESS, GET_OPERATION_REPORT_LIST_FAIL, GET_OPERATION_REPORT_LIST_REQUEST, GET_OPERATION_REPORT_LIST_SUCCESS, GET_SAFETY_DECLARATION_REPORT_LIST_FAIL, GET_SAFETY_DECLARATION_REPORT_LIST_REQUEST, GET_SAFETY_DECLARATION_REPORT_LIST_SUCCESS, GET_SITE_USER_SMARTPHONE_REPORT_LIST_FAIL, GET_SITE_USER_SMARTPHONE_REPORT_LIST_REQUEST, GET_SITE_USER_SMARTPHONE_REPORT_LIST_SUCCESS, GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_FAIL, GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_REQUEST, GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_SUCCESS, GET_SP_SAFETY_DECLARATION_REPORT_LIST_FAIL, GET_SP_SAFETY_DECLARATION_REPORT_LIST_REQUEST, GET_SP_SAFETY_DECLARATION_REPORT_LIST_SUCCESS, GET_SUB_ADMIN_REPORT_LIST_FAIL, GET_SUB_ADMIN_REPORT_LIST_REQUEST, GET_SUB_ADMIN_REPORT_LIST_SUCCESS, GET_USER_ACCESS_REPORT_LIST_FAIL, GET_USER_ACCESS_REPORT_LIST_REQUEST, GET_USER_ACCESS_REPORT_LIST_SUCCESS } from '../contsants/reportConstants';

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getUserAccessReportList = (siteId, fromDate, toDate) => async (dispatch) => {
    try {
        dispatch({
            type: GET_USER_ACCESS_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getQRCodeReportBySiteId?siteId=' + siteId + '&fromDate=' + fromDate + '&toDate=' + toDate,
            config
        ) 

        dispatch({
            type: GET_USER_ACCESS_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_USER_ACCESS_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getSafetyDeclarationReportList = (siteId, fromDate, toDate) => async (dispatch) => {
    try {
        dispatch({
            type: GET_SAFETY_DECLARATION_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getSafetyDeclarationReportBySiteId?siteId=' + siteId + '&fromDate=' + fromDate + '&toDate=' + toDate,
            config
        ) 

        dispatch({
            type: GET_SAFETY_DECLARATION_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_SAFETY_DECLARATION_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getSmartPhoneUserAccessReportList = (siteId, fromDate, toDate) => async (dispatch) => {
    try {
        dispatch({
            type: GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getDeviceLogsReportBySiteId?siteId=' + siteId + '&fromDate=' + fromDate + '&toDate=' + toDate,
            config
        ) 

        dispatch({
            type: GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_SMARTPHONE_USER_ACCESS_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getSpSafetyDeclarationReportList = (siteId, fromDate, toDate) => async (dispatch) => {
    try {
        dispatch({
            type: GET_SP_SAFETY_DECLARATION_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getSafetyDeclarationMobileReportBySiteId?siteId=' + siteId + '&fromDate=' + fromDate + '&toDate=' + toDate,
            config
        ) 

        dispatch({
            type: GET_SP_SAFETY_DECLARATION_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_SP_SAFETY_DECLARATION_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getSiteUserSmartPhoneReportList = (siteId, fromDate, toDate) => async (dispatch) => {
    try {
        dispatch({
            type: GET_SITE_USER_SMARTPHONE_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getSmartPhoneUserReportBySiteId?siteId=' + siteId + '&fromDate=' + fromDate + '&toDate=' + toDate,
            config
        ) 

        dispatch({
            type: GET_SITE_USER_SMARTPHONE_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_SITE_USER_SMARTPHONE_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getOperationReportList = (siteId, fromDate, toDate) => async (dispatch) => {
    try {
        dispatch({
            type: GET_OPERATION_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getChangeLogReportBySiteId?siteId=' + siteId + '&fromDate=' + fromDate + '&toDate=' + toDate,
            config
        ) 

        dispatch({
            type: GET_OPERATION_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_OPERATION_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

// Super Admin

export const getAdminReportList = (siteName) => async (dispatch) => {
    try {
        dispatch({
            type: GET_ADMIN_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getAdminReportBySiteName?siteName=' + siteName,
            config
        ) 

        dispatch({
            type: GET_ADMIN_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_ADMIN_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getSubAdminReportList = (siteName) => async (dispatch) => {
    try {
        dispatch({
            type: GET_SUB_ADMIN_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getSubAdminReportBySiteName?siteName=' + siteName,
            config
        ) 

        dispatch({
            type: GET_SUB_ADMIN_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_SUB_ADMIN_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getLoggedUserReportList = (siteId) => async (dispatch) => {
    try {
        dispatch({
            type: GET_LOGGED_USER_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getLoggedInUsersBySiteId?siteId=' + siteId,
            config
        ) 

        dispatch({
            type: GET_LOGGED_USER_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_LOGGED_USER_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getDeviceReportList = (siteId, fromDate, toDate) => async (dispatch) => {
    try {
        dispatch({
            type: GET_DEVICE_REPORT_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Report/getDeviceLogsBySiteId?siteId=' + siteId + '&fromDate=' + fromDate + '&toDate=' + toDate,
            config
        ) 

        dispatch({
            type: GET_DEVICE_REPORT_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: GET_DEVICE_REPORT_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}