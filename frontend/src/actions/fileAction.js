import axios from 'axios'
import { DELETE_MEDIA_FILE_FAIL, DELETE_MEDIA_FILE_REQUEST, DELETE_MEDIA_FILE_SUCCESS } from '../contsants/fileConstants';

const apiUrl = process.env.REACT_APP_BASE_URL;

export const deleteMediaFile = (params) => async (dispatch) => {
    try {
        dispatch({
            type: DELETE_MEDIA_FILE_REQUEST
        });

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + params.token
            }
        };

        const { data } = await axios.delete(
            apiUrl + 'api/Setup/DeleteFiles?fileId=' + params.fileId + 
                '&siteId=' + params.siteId + '&categoryId=' + params.categoryId 
                + '&category=' + params.category + '&fileType=' + params.type ,
            config
        )

        dispatch({
            type: DELETE_MEDIA_FILE_SUCCESS,
            payload: params
        })
    } catch (error) {
        dispatch({
            type: DELETE_MEDIA_FILE_FAIL,
            payload:
                error.response && error.response.data.title
                    ? error.response.data.title
                    : error.message
        })
    }
}
