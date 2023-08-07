import {
    SITE_INFORMATION_CREATE_REQUEST,
    SITE_INFORMATION_CREATE_FAIL,
    SITE_INFORMATION_CREATE_SUCCESS,
    SITE_CODE_CREATE_REQUEST,
    SITE_CODE_CREATE_SUCCESS,
    SITE_CODE_CREATE_FAIL,
    SITE_ALL_LIST_REQUEST,
    SITE_ALL_LIST_SUCCESS,
    SITE_ALL_LIST_FAIL,
    SITE_UNASSIGNED_LIST_REQUEST,
    SITE_UNASSIGNED_LIST_SUCCESS,
    SITE_UNASSIGNED_LIST_FAIL,
    SITE_CODE_DELETE_REQUEST,
    SITE_CODE_DELETE_SUCCESS,
    SITE_CODE_DELETE_FAIL,
    SITE_CODE_VALIDATION_REQUEST,
    SITE_CODE_VALIDATION_SUCCESS,
    SITE_CODE_VALIDATION_FAIL,
    SITE_LIST_REQUEST,
    SITE_LIST_SUCCESS,
    SITE_LIST_FAIL,
    SITE_INFORMATION_DETAIL_SUCCESS,
    SITE_INFORMATION_DETAIL_FAIL,
    SITE_INFORMATION_DETAIL_REQUEST,
    ALL_SITES_SPACE_LIST_REQUEST,
    ALL_SITES_SPACE_LIST_SUCCESS,
    ALL_SITES_SPACE_LIST_FAIL,
    SITE_SPACE_DETAIL_SUCCESS,
    SITE_SPACE_DETAIL_FAIL,
    SITE_SPACE_DETAIL_REQUEST,
    SITE_DETAIL_BY_QR_REQUEST,
    SITE_DETAIL_BY_QR_SUCCESS,
    SITE_DETAIL_BY_QR_FAIL,
    SITE_INFORMATION_UPDATE_REQUEST,
    SITE_INFORMATION_UPDATE_SUCCESS,
    SITE_INFORMATION_UPDATE_FAIL,
    ASSIGN_SITE_SPACE_REQUEST,
    ASSIGN_SITE_SPACE_SUCCESS,
    ASSIGN_SITE_SPACE_FAIL
} from '../contsants/siteConstants'
import axios from 'axios'

const apiUrl = process.env.REACT_APP_BASE_URL;

// Super Admin

export const createSiteCode = (siteCode, userId, currentUserId) => async (dispatch) => {
    try {
        dispatch({
            type: SITE_CODE_CREATE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/SiteCodeAccess/generateCode',
            {siteCode, userId, currentUserId},
            config
        )

        if (data.isSuccess == true) {
            dispatch({
                type: SITE_CODE_CREATE_SUCCESS
            })
        }

        dispatch(listAllSiteCode())
    } catch (error) {
        dispatch({
            type: SITE_CODE_CREATE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const listAllSiteCode = () => async (dispatch) => {
    try {
        dispatch({
            type: SITE_ALL_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/SiteCodeAccess/siteCodeList',
            config
        )

        let assigned = data.filter((code) => code.isOccupied == true)
        let unassigned = data.filter((code) => code.isOccupied == false)

        dispatch({
            type: SITE_ALL_LIST_SUCCESS,
            payload: {
                assigned: assigned,
                unassigned: unassigned
            }
        })
    } catch (error) {
        dispatch({
            type: SITE_ALL_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const deleteSiteCode = (params) => async (dispatch, getState) => {
    try {
        dispatch({
            type: SITE_CODE_DELETE_REQUEST
        });
    
        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/SiteCodeAccess/siteCodeDelete?siteCodeId=' + params.id,
            config
        )
    
        dispatch({
            type: SITE_CODE_DELETE_SUCCESS
        });
        dispatch(listAllSiteCode())
    } catch (error) {
        dispatch({
            type: SITE_CODE_DELETE_FAIL,
            payload:
            error.response && error.response.data.message
                ? error.response.data.message
                : error.message
        });
    }
};

// Admin / Sub Admin

export const listSiteCode = (userId) => async (dispatch) => {
    try {
        dispatch({
            type: SITE_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Site/getSitesByAdmin?userId=' + userId,
            config
        )

        dispatch({
            type: SITE_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: SITE_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const validateSiteCode = (code) => async (dispatch) => {    
    try {
        dispatch({
            type: SITE_CODE_VALIDATION_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/SiteCodeAccess/accessSiteCreation?code=' + code,
            config
        )

        dispatch({
            type: SITE_CODE_VALIDATION_SUCCESS,
            payload: { 
                isSuccess: data,
                siteCode: code
            }
        })
    } catch (error) {
        dispatch({
            type: SITE_CODE_VALIDATION_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const createSite = (params) => async (dispatch) => {
    try {
        dispatch({
            type: SITE_INFORMATION_CREATE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const response = await axios.get(
            `https://maps.googleapis.com/maps/api/geocode/json?latlng=${params.latitude},${params.longitude}&key=AIzaSyBeHiA7IBPch3gqpcPpkzupx58eMqy7qNk`
        );

        const { results } = response.data;
        
        var address = ''
        var city = ''
        var country = ''

        if (results.length > 0) {
            const addressComponents = results[0].address_components;
            
            const formattedAddress = results[0].formatted_address;
            address = formattedAddress;

            for (let i = 0; i < addressComponents.length; i++) {
                const component = addressComponents[i];
                const types = component.types;

                if (types.includes('country')) {
                    country = component.long_name;
                }

                if (types.includes('locality') || types.includes('administrative_area_level_1')) {
                    city = component.long_name;
                }
            }
        }

        const { data } = await axios.post(
            apiUrl + 'api/Site/siteCreate',
            { 
                siteName: params.siteName,  
                address: address,
                city: city,
                country: country,
                periodStart: params.periodStart,
                periodEnd: params.periodEnd,
                browseTimeFrom: params.browseTimeFrom,
                browseTimeTo: params.browseTimeTo,
                gpsRange: params.gpsRange,
                siteAccessCode: params.siteAccessCode,
                userId: params.userId,
                userName: params.userName,
                qrCodeValue: params.qrCodeValue
            },
            config
        )


        dispatch({
            type: SITE_INFORMATION_CREATE_SUCCESS,
            payload: { 
                isSuccess: data
            }
        })
    } catch (error) {
        dispatch({
            type: SITE_INFORMATION_CREATE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const updateSite = (params) => async (dispatch) => {
    try {
        dispatch({
            type: SITE_INFORMATION_UPDATE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const response = await axios.get(
            `https://maps.googleapis.com/maps/api/geocode/json?latlng=${params.latitude},${params.longitude}&key=AIzaSyBeHiA7IBPch3gqpcPpkzupx58eMqy7qNk`
        );

        const { results } = response.data;
        
        var address = ''
        var city = ''
        var country = ''

        if (results.length > 0) {
            const addressComponents = results[0].address_components;
            
            const formattedAddress = results[0].formatted_address;
            address = formattedAddress;

            for (let i = 0; i < addressComponents.length; i++) {
                const component = addressComponents[i];
                const types = component.types;

                if (types.includes('country')) {
                    country = component.long_name;
                }

                if (types.includes('locality') || types.includes('administrative_area_level_1')) {
                    city = component.long_name;
                }
            }
        }

        const { data } = await axios.put(
            apiUrl + 'api/Site/siteUpdate',
            { 
                id: params.id,
                siteName: params.siteName,  
                address: address,
                city: city,
                country: country,
                periodStart: params.periodStart,
                periodEnd: params.periodEnd,
                browseTimeFrom: params.browseTimeFrom,
                browseTimeTo: params.browseTimeTo,
                gpsRange: params.gpsRange,
                userId: params.userId,
                userName: params.userName,
                qrCodeValue: params.qrCodeValue
            },
            config
        )


        dispatch({
            type: SITE_INFORMATION_UPDATE_SUCCESS,
            payload: { 
                isSuccess: data
            }
        })
    } catch (error) {
        dispatch({
            type: SITE_INFORMATION_UPDATE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getSiteDetail = (id) => async (dispatch) => {
    try {
        dispatch({
            type: SITE_INFORMATION_DETAIL_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/Site/getSiteDetails?siteId=' + id,
            config
        )

        dispatch({
            type: SITE_INFORMATION_DETAIL_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: SITE_INFORMATION_DETAIL_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getAllAggregateSiteSpaces = () => async (dispatch) => {
    try {
        dispatch({
            type: ALL_SITES_SPACE_LIST_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/SiteSpace/aggregateSiteSpaces',
            config
        )

        dispatch({
            type: ALL_SITES_SPACE_LIST_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: ALL_SITES_SPACE_LIST_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const getSiteSpaceDetail = (id) => async (dispatch) => {
    try {
        dispatch({
            type: SITE_SPACE_DETAIL_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.get(
            apiUrl + 'api/SiteSpace/aggregateSiteSpace?siteId=' + id,
            config
        )

        dispatch({
            type: SITE_SPACE_DETAIL_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: SITE_SPACE_DETAIL_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}

export const assignSiteSpaceFunction = (id, siteName, siteStorage) => async (dispatch) => {
    try {
        dispatch({
            type: ASSIGN_SITE_SPACE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        const { data } = await axios.post(
            apiUrl + 'api/SiteSpace/aggregateSiteSpacesAssign?siteId=' + id + '&space=' + siteStorage,
            {
                siteId: id,
                space: siteStorage
            },
            config
        )

        dispatch({
            type: ASSIGN_SITE_SPACE_SUCCESS,
            payload: data
        })
        dispatch(getAllAggregateSiteSpaces())
    } catch (error) {
        dispatch({
            type: ASSIGN_SITE_SPACE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}


export const getSiteDetailsForUserByQR = (qrCode, latitude, longitude) => async (dispatch) => {
    try {
        dispatch({
            type: SITE_DETAIL_BY_QR_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            }
        };

        qrCode = "071aa934-c8c2-4907-1769-83f7c7e2ffe8"
        latitude = 43.07561405433993
        longitude = 141.35035309523923
        const time = new Date();
        const currentTime = `${time.getHours()}:${time.getMinutes()}:${time.getSeconds()}`

        // Different Latitude and Longitude does not work as of now Currently Nepal
        // latitude = "27.262814883568208"
        // longitude = "84.86002716750487"

        const { data } = await axios.get(
            apiUrl + 'api/Site/getSiteDetailsForUsersByQR?QRCode=' + qrCode + 
            "&latitude=" + latitude + "&longitude=" + longitude + "&currentTime=" + currentTime,
            config
        )

        dispatch({
            type: SITE_DETAIL_BY_QR_SUCCESS,
            payload: data
        })
    } catch (error) {
        dispatch({
            type: SITE_DETAIL_BY_QR_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}