import { toast } from 'react-toastify';
const apiUrl = process.env.REACT_APP_BASE_URL;

export function toastUI(data, setIsLoading, msg, type) {
    setIsLoading(data.loading)
    if (data.error) {
        toast.error(data.error, {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    } else if (data.success) {
        toast.success( msg + ' has been successfully ' + type , {
            position: toast.POSITION.TOP_RIGHT
        })
        return true
    }
}

export function getColumns(setColumns, obj) {
    let columns = []

    Object.entries(obj).map(([key,value]) => {
        columns.push(generateHeader(key, value))
    })

    setColumns(columns)
}

function generateHeader(acc, title) {
    let headerObj = {
        Header: () => (
            <a>
                {title}
            </a>
        ),
        accessor: acc
    }
    return headerObj
}

export function manipulateReportData(type, list, reportData) {
    reportData = []
    let no = 1
    list.list.map((data) => {
        let obj
        switch (type) {
            case "spsd":
                obj = {
                    sNo: no,
                    fullName: data.fullName_Kanji,
                    kanaName: data.fullName_Kana,
                    email: data.email,
                    browseDate: data.entryDate,
                    browseTime: data.entryTime,
                    companyName: data.companyName
                }
                break;
            case "ol":
                obj = {
                    sNo: no,
                    category: data.category,
                    changeType: data.changeType,
                    changeCategory: data.changeCategory,
                    changedProperty: data.changedProperties,
                    email: data.email,
                    browseDate: data.date,
                    browseTime: data.time
                }
                break;
            case "ua":
                obj = {
                    sNo: no,
                    fullName: data.fullName_Kanji,
                    kanaName: data.fullName_Kana,
                    email: data.email,
                    browseDate: data.entryDate,
                    browseTime: data.entryTime,
                    companyName: data.companyName
                }
                break;
            case "sd":
                obj = {
                    sNo: no,
                    fullName: data.fullName_Kanji,
                    kanaName: data.fullName_Kana,
                    email: data.email,
                    browseDate: data.entryDate,
                    browseTime: data.entryTime,
                    companyName: data.companyName,
                    status: data.is_Checked
                }
                break;
            case "sua":
                obj = {
                    sNo: no,
                    phoneNumber: data.phoneNumber,
                    browseDate: data.date,
                    browseTime: data.time,
                    siteName: data.siteName
                }
                break;
            case "susp":
                obj = {
                    sNo: no,
                    username: data.userName,
                    companyName: data.companyName,
                    phoneNumber: data.phoneNumber,
                    siteName: data.siteName
                }
                break;
            case "ad":
                obj = {
                    sNo: no,
                    sitename: data.siteName,
                    kananame: data.fullName_Kana,
                    kanjiname: data.fullName_Kanji,
                    email: data.email,
                    setperiod: data.siteTimePeriod,
                    dateofregister: data.siteCreatedDate
                }
                break;
            case "sba":
                obj = {
                    sNo: no,
                    sitename: data.siteName,
                    kananame: data.fullName_Kana,
                    kanjiname: data.fullName_Kanji,
                    email: data.email,
                    setperiod: data.siteTimePeriod,
                    dateofregister: data.joinedOn   
                }
                break;
            case "dl":
                obj = {
                    sNo: no,
                    phoneno: data.phoneNumber,
                    browsedate: data.date,
                    sitename: data.siteName
                }
                break;
            case "liul":
                obj = {
                    sNo: no,
                    sitename: data.siteName,
                    kananame: data.fullName_Kana,
                    kanjiname: data.fullName_Kanji,
                    email: data.email,
                    setperiod: data.siteTimePeriod,
                    dateofregister: data.joinedOn   
                }
                break;
        }

        no++
        reportData.push(obj)
    })
    return reportData
} 

export async function userBoardFileData(data, userInfo, setData) {
    const responseData = await Promise.all(data.map(info => fetchOnlyPhoto(info, userInfo)));

    const filteredData = responseData.filter(data => data !== null);

    setData(filteredData)
}

async function fetchOnlyPhoto(info, userInfo) {
    let no = 1;
    let url;

    url = apiUrl + 'api/Setup/GetFiles?filePath=' + info?.image?.filePath;

    let obj = {
        no: no,
        title: info.title,
        id: info.id,
        description: info.description,
        uniqueId: info?.image?.photoId,
        blob: null,
        url: null
    };
    no++;

    try {
    const response = await fetch(url, {
        headers: {
            Authorization: 'Bearer ' + userInfo.token,
        },
    });

    if (response.ok) {
        const blob = await response.blob();
        const blobUrl = URL.createObjectURL(blob)

        obj.blob = blob
        obj.url = blobUrl

        return obj;
        } else {
            console.error('Failed to fetch:', response.status);
            return obj;
        }
    } catch (error) {
        console.error('Error fetching data:', error);
        return obj;
    }


}

export async function manipulateFileData(fileData, type, userInfo, setPhotoData, setVideoData, setPdfData) {
    const responseData = await Promise.all(fileData.map(info => fetchPdfData(info, type, userInfo)));

    const filteredData = responseData.filter(data => data !== null);

    if (type == 'image/png') {
        setPhotoData(filteredData);
    } else if (type == 'video/mp4') {
        setVideoData(filteredData);
    } else {
        setPdfData(filteredData);
    }
}

async function fetchPdfData(info, type, userInfo) {
    let no = 1;
    let url;

    if (type == 'image/png') {
        url = apiUrl + 'api/Setup/GetFiles?filePath=' + info.filePath;
    } else if (type == 'video/mp4') {
        url = apiUrl + 'api/Setup/GetFiles?filePath=' + info.videoFilePath;
    } else {
        url = apiUrl + 'api/Setup/GetFiles?filePath=' + info.filePath;
    }

    try {
    const response = await fetch(url, {
        headers: {
            Authorization: 'Bearer ' + JSON.parse(userInfo).token,
        },
    });

    if (response.ok) {
        const blob = type == "application/pdf" ? await new Blob([await response.arrayBuffer()], { type: type }) : await response.blob();
        const blobUrl = URL.createObjectURL(blob)
        // ;type == 'image/png'
        //     ? apiUrl + 'api/Setup/GetFiles?filePath=' + info.filePath
        //     : type == 'video/mp4'
        //     ? apiUrl + 'api/Setup/GetFiles?filePath=' + info.videoFilePath
        //     : URL.createObjectURL(blob);

        let obj = {
            id: no,
            uniqueId: type == 'image/png' ? info.photoId : type == 'video/mp4' ? info.videoId : info.fileUploadId,
            blob: blob,
            url: blobUrl // Use the object URL to display the PDF in the browser
        };

        no++;
        return obj;
        } else {
            console.error('Failed to fetch:', response.status);
            return null;
        }
    } catch (error) {
        console.error('Error fetching data:', error);
        return null;
    }
}

export function blobConversion(finalData, previousData, newData) {
    previousData.map((data) => {
        finalData.push(data.blob)
    })
    newData.map((data) => {
        let blob = new Blob([data], { type: data.type })
        finalData.push(blob)
    })
}

export function validation(email, password, confirmPassword, termsRead) {
    if (!email) {
        toast.error('Email is required !!', {
            position: toast.POSITION.TOP_RIGHT
        });
        return false
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email)) {
        toast.error('Invalid email address', {
            position: toast.POSITION.TOP_RIGHT
        });
        return false
    } else if (!password) {
        toast.error('Password is required !!', {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    } else if (password.length < 6) {
        toast.error('Password must be at least 6 characters', {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    } else if (password !== confirmPassword) {
        toast.error('Confirm password is not matched', {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    } else if (termsRead == false) {
        toast.error('Please read terms and conditions before proceding.', {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    }
    return true
}

export function validateEmail(email) {
    if (!email) {
        toast.error('Email is required !!', {
            position: toast.POSITION.TOP_RIGHT
        });
        return false
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email)) {
        toast.error('Invalid email address', {
            position: toast.POSITION.TOP_RIGHT
        });
        return false
    }
    return true
}

export function validatePassword(password, confirmPassword) {
    if (!password) {
        toast.error('Password is required !!', {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    } else if (password.length < 6) {
        toast.error('Password must be at least 6 characters', {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    } else if (password !== confirmPassword) {
        toast.error('Confirm password is not matched', {
            position: toast.POSITION.TOP_RIGHT
        })
        return false
    }
    return true
}

export function validateForReset(email, password, confirmPassword) {
    let emailValidated = validateEmail(email)
    let passwordValidated = validatePassword(password, confirmPassword)
    if (emailValidated == false) {
        return false
    } else if (passwordValidated == false) {
        return false
    }
    return true
}