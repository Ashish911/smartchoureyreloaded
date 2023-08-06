import React,{ useEffect, useMemo, useState } from 'react'
import { useGlobalFilter, useMountedLayoutEffect, useRowSelect, useSortBy, useTable } from 'react-table'
import { FaSortDown, FaSortUp, FaSort, FaSearch } from "react-icons/fa";
import { CiCircleRemove } from "react-icons/ci"

const GlobalFilter = ({globalFilter, setGlobalFilter}) => {
    return(
        <input type="text" placeholder="Search" 
        value = {globalFilter}
        onChange={e => {
            setGlobalFilter(e.target.value)
        }}
        className="px-4 py-3 rounded-md hover:bg-gray-100 lg:max-w-sm md:py-2 md:flex-1 
        focus:outline-none md:focus:bg-gray-100 md:focus:shadow md:focus:border"/>
    )
}

const CheckBoxInput = React.forwardRef(
    ({isSelected, toggleRowSelected, toggleAllRowsSelected}) => {
        return (
            <label className='dw-checkbox' onClick={(e) => {
                e.preventDefault()
                e.stopPropagation()
                if(!isSelected == false) {
                    toggleRowSelected(!isSelected)
                } else {
                    toggleRowSelected()
                }
                // toggleRowSelected(!isSelected)
            }}>
                <input type='checkbox' checked={isSelected} onChange={() => null} />
                <span className='dw-checkmark'/>
            </label>
        )
    }
)

const AdvancedTable = ({columns, data, createText, createFunction, enableSelect, enableEdit, editFunction, detailFunction, multiDeleteFunction}) => {
    
    const mandatoryColumns = [
        {
            id: 'details',
            Header: () => (
                <a>
                        Detail
                </a>
            ),
            Cell: ({row}) => (
                <a onClick={() => {
                    detailFunction(row)
                }}
                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                bg-gray-200 rounded-md shadow hover:bg-opacity-20">
                    <FaSearch />
                    <span>Detail</span>
                </a>
            ),
            width: 60
        }, {
            id: 'delete',
            Header: () => (
                <a>
                    Delete
                </a>
            ),
            Cell: ({row}) => (
                <a onClick={() => {
                    multiDeleteFunction(row)
                }}
                className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                bg-gray-200 rounded-md shadow hover:bg-opacity-20">
                    <CiCircleRemove />
                    <span>Delete</span>
                </a>
            ),
            width: 60
        }
    ]

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow,
        selectedFlatRows,
        toggleAllRowsSelected,
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
            useRowSelect,
            hooks => {
                enableEdit ? 
                hooks.visibleColumns.push(columns => [
                    {
                        id: 'selection',
                        Header: ({toggleAllRowsSelected, selectedFlatRows}) => (
                            <a onClick={toggleAllRowsSelected.bind(this, !selectedFlatRows?.length > 0)}
                            className='checkAll text-dark'>
                                <strong>
                                    Select<br/>All
                                </strong>
                            </a>
                        ),
                        Cell: ({row}) => (
                            <>
                                <CheckBoxInput {...row} toggleAllRowsSelected={toggleAllRowsSelected} />
                            </>
                        ),
                        width: 60
                    },
                    ...columns,
                    { 
                        id: 'Edit',
                        Header: () => (
                            <a>
                                Edit
                            </a>
                        ),
                        Cell: ({row}) => (
                            <a onClick={() => {
                                editFunction(row)
                            }}
                            className="inline-flex items-center justify-center px-4 py-1 space-x-1 
                            bg-gray-200 rounded-md shadow hover:bg-opacity-20">
                                <FaSearch />
                                <span>Edit</span>
                            </a>
                        ),
                        width: 60
                    },
                    ...mandatoryColumns
                ]) : hooks.visibleColumns.push(columns => [
                    ...columns,
                    ...mandatoryColumns
                ])
        }
    )

    useEffect(() => {
        if (selectedFlatRows.length > 0) {
            let selectData = []
            selectedFlatRows.map((id) => {
                selectData.push(id.original.id)
            })
        }
    }, [selectedFlatRows])

    return (
        <>
        <div className='flex flex-col items-start justify-between ml-1.5 pb-6 space-y-4 lg:items-center lg:space-y-0 lg:flex-row'>
            <GlobalFilter
                globalFilter={state.globalFilter}
                setGlobalFilter= {setGlobalFilter}
            />
            <div>
            {selectedFlatRows.length > 0 ?
                <a
                className="inline-flex items-center justify-center px-4 py-1 mr-2 space-x-1 
                bg-gray-200 rounded-md shadow hover:bg-opacity-20" onClick={() => {
                    multiDeleteFunction(selectedFlatRows)
                }}>
                    <span>Delete Selected</span>
                </a>
            :
                ''
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
            
        </div>
        <div className="flex flex-col">
            <div className="overflow-x-auto">
                <div className="p-1.5 w-full inline-block align-middle">
                    <div className="overflow-hidden border rounded-lg">
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
                            <tr {...row.getRowProps({
                                label: row.original.id,
                                className: row.isSelected ? 'lightSelected' : 'selected-tr-hover',
                                onClick: e => {
                                    row.isSelected ? row.toggleRowSelected(false) : row.toggleRowSelected()
                                }
                            })}>
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

export default AdvancedTable