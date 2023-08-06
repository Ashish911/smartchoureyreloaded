import React,{ useEffect, useMemo, useState } from 'react'
import { useGlobalFilter, useSortBy, useTable } from 'react-table'
import { FaSortDown, FaSortUp, FaSort, FaSearch } from "react-icons/fa";
import { CiCircleRemove } from "react-icons/ci"
import { useTranslation } from 'react-i18next'
import { utils, write } from 'xlsx';
import { MdOutlineAssignmentTurnedIn } from 'react-icons/md'

const GlobalFilter = ({globalFilter, setGlobalFilter}) => {
    const {t} = useTranslation()
    
    return(
        <input type="text" placeholder={t('Search')}
        value = {globalFilter}
        onChange={e => {
            setGlobalFilter(e.target.value)
        }}
        className="px-4 py-3 rounded-md hover:bg-gray-100 lg:max-w-sm md:py-2 md:flex-1 
        focus:outline-none md:focus:bg-gray-100 md:focus:shadow md:focus:border"/>
    )
}

const GlobalTable = ({columns, data, createText, createFunction, enableDetail, detailFunction, enableEdit, editFunction, fileName, sheetName, enableDelete, deleteFunction, enableAssign, assignFunction}) => {

    const {t} = useTranslation()

    let mandatoryColumns = []

    if (enableEdit == true) {
        mandatoryColumns.push({
            id: 'edit',
            Header: () => (
                <a>
                        {t('Edit')}
                </a>
            ),
            Cell: ({row}) => (
                <a onClick={() => {
                    editFunction(row)
                }}
                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                bg-gray-200 rounded-md shadow hover:bg-opacity-20">
                    <FaSearch />
                    <span>{t('Edit')}</span>
                </a>
            ),
            width: 60
        })
    }

    if (enableDetail == true) {
        mandatoryColumns.push({
            id: 'details',
            Header: () => (
                <a>
                        {t('Detail')}
                </a>
            ),
            Cell: ({row}) => (
                <a onClick={() => {
                    detailFunction(row)
                }}
                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                bg-gray-200 rounded-md shadow hover:bg-opacity-20">
                    <FaSearch />
                    <span>{t('Detail')}</span>
                </a>
            ),
            width: 60
        })
    }

    if (enableAssign == true) {
        mandatoryColumns.push({
            id: 'assign',
            Header: () => (
                <a>
                    {t('Assign')}
                </a>
            ),
            Cell: ({row}) => (
                <a onClick={() => {
                    assignFunction(row)
                }}
                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                bg-gray-200 rounded-md shadow hover:bg-opacity-20">
                    <MdOutlineAssignmentTurnedIn />
                    <span>{t('Assign')}</span>
                </a>
            ),
            width: 60
        })
    }

    if (enableDelete == true) {
        mandatoryColumns.push({
            id: 'delete',
            Header: () => (
                <a>
                    {t('Delete')}
                </a>
            ),
            Cell: ({row}) => (
                <a onClick={() => {
                    deleteFunction(row)
                }}
                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                bg-gray-200 rounded-md shadow hover:bg-opacity-20">
                    <CiCircleRemove />
                    <span>{t('Delete')}</span>
                </a>
            ),
            width: 60
        })
    } 


    const onDownload = () => {

    // Get the data from your React table
    const tableData = rows.map(row => row.original);

    // Create a new workbook and worksheet
    const workbook = utils.book_new();
    const worksheet = utils.json_to_sheet(tableData);

    // Add the worksheet to the workbook
    utils.book_append_sheet(workbook, worksheet, 'Sheet 1');

    // Convert the workbook to a binary string
    const excelData = write(workbook, { bookType: 'xlsx', type: 'binary' });

    // Create a Blob object from the binary string
    const blob = new Blob([s2ab(excelData)], { type: 'application/octet-stream' });

    // Create a download link and trigger the download
    const downloadLink = document.createElement('a');
    downloadLink.href = URL.createObjectURL(blob);
    downloadLink.download = 'tableData.xlsx';
    downloadLink.click();
    }

    const s2ab = (s) => {
        const buf = new ArrayBuffer(s.length);
        const view = new Uint8Array(buf);
        for (let i = 0; i < s.length; i++) {
            view[i] = s.charCodeAt(i) & 0xff;
        }
        return buf;
    };

    const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    rows,
    prepareRow,
    setGlobalFilter,
    state
    } = useTable(
        { 
            columns, 
            data,
            sortTypes: useMemo(() => (rowA, rowB, columnId) => {
                const a = rowA.original[columnId].toLowerCase()
                const b = rowB.original[columnId].toLowerCase()

                if (a > b) 
                    return 1

                if (b > a) 
                    return -1

                return 0
            }) 
        },
        useGlobalFilter,
        useSortBy,
        hooks => {
            hooks.visibleColumns.push(columns => [
                ...columns,
                ...mandatoryColumns
            ])
        }
    )

    return (
        <>
            <div className='flex flex-col items-start justify-between ml-1.5 pb-6 space-y-4 lg:items-center lg:space-y-0 lg:flex-row'>
                <GlobalFilter
                    globalFilter={state.globalFilter}
                    setGlobalFilter= {setGlobalFilter}
                />
                {sheetName &&
                    <a
                    className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                    bg-gray-200 rounded-md shadow hover:bg-opacity-20 cursor-pointer" onClick={onDownload}>
                        <span>Download</span>
                    </a>
                }
                {createText != undefined
                ? 
                    <a
                    className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                    bg-gray-200 rounded-md shadow hover:bg-opacity-20" onClick={createFunction}>
                        <span>{createText}</span>
                    </a>
                :
                    ''
                }
            </div>
            <div className="flex flex-col">
                <div>
                    <div className="p-1.5 w-full inline-block align-middle">
                        <div className="border rounded-lg overflow-x-auto global-table">
                            <table {...getTableProps()} className="min-w-full divide-y divide-gray-200">
                            <thead className='bg-gray-50'>
                            {headerGroups.map(headerGroup => (
                                <tr {...headerGroup.getHeaderGroupProps()}>
                                {headerGroup.headers.map(column => (
                                    <th
                                    {...column.getHeaderProps(column.getSortByToggleProps())}
                                    scope="col"
                                    className="px-6 py-3 text-xs font-bold text-left text-gray-500 uppercase "
                                    >
                                    <span className='flex justify-center items-center'>
                                        {column.render('Header')}
                                        {['details'].includes(column.id)? '' : (column.isSorted ? (
                                            column.isSortedDesc ? (
                                                <FaSortDown />
                                            ) : (
                                                <FaSortUp />
                                            )
                                        ) : (
                                            <FaSort />
                                        ))}
                                    </span>
                                    </th>
                                ))}
                                </tr>
                            ))}
                            </thead>
                            <tbody {...getTableBodyProps()} className="divide-y divide-gray-200 bg-white">
                            {rows.map(row => {
                                prepareRow(row)
                                return (
                                <tr {...row.getRowProps()}>
                                    {row.cells.map(cell => {
                                    return (
                                        <td
                                        {...cell.getCellProps()}
                                        className="px-6 py-4 text-sm text-gray-800 whitespace-nowrap"
                                        >
                                            {['enable'].includes(cell.column.id)? 
                                                cell.value == true ?
                                                <a 
                                                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                                                bg-green-500 text-white rounded-md shadow">
                                                    <span>Enabled</span>
                                                </a> 
                                                :
                                                <a 
                                                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                                                bg-red-500 text-white rounded-md shadow">
                                                    <span>Disabled</span>
                                                </a>
                                            : cell.render('Cell')}
                                        </td>
                                    )
                                    })}
                                </tr>
                                )
                            })}
                            </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default GlobalTable