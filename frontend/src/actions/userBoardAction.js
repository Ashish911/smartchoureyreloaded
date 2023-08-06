import { GET_USERBOARD_FAIL, GET_USERBOARD_REQUEST, GET_USERBOARD_SUCCESS } from "../contsants/userBoardConstants";
import axios from 'axios'

const apiUrl = process.env.REACT_APP_BASE_URL;

export const getUserBoard = (siteId, userId) => async (dispatch) => {
    try {
        dispatch({
            type: GET_USERBOARD_REQUEST
        })
        
        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        axios.defaults.withCredentials = true;

        const { data } = await axios.get(
            apiUrl + 'api/Userboard/getUserboard',
            config
        )

        dispatch({
            type: GET_USERBOARD_SUCCESS,
            payload: data
        })

    } catch (error) {
        dispatch({
            type: GET_USERBOARD_FAIL,
            payload:
                error.response && error.response.data.message
                    ? error.response.data.message
                    : error.message
        })
    }
}