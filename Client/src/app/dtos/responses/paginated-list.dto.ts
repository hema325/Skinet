export interface PaginatedListDto<TEntity> {
    data: TEntity[]
    pageNumber: number
    pageSize: number
    totalPages: number
    hasNextpage: boolean
    hasPrevPage: boolean
}
