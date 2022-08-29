﻿using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     Audit log request validator
/// </summary>
public class AuditLogDomainRequestValidator : LogRequestBaseValidator<AuditLogFilterDto, LogSortDto, AuditLogDomainModel>
{
    public AuditLogDomainRequestValidator(IValidator<PaginationRequestDto> paginationRequestValidator, IValidator<ILogFilter> logFilterValidator, IpAddressValidator ipAddressValidator) : base(paginationRequestValidator, logFilterValidator)
    {
        When(model => !string.IsNullOrEmpty(model.Filter.Ip), () =>
        {
            RuleFor(requestDto => requestDto.Filter.Ip).SetValidator(ipAddressValidator!);
        });
    }
}