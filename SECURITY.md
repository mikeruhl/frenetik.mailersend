# Security Policy

## Supported Versions

| Version | Supported          |
| ------- | ------------------ |
| 1.x.x   | :white_check_mark: |

## Reporting a Vulnerability

If you discover a security vulnerability in this SDK, please report it responsibly:

1. **DO NOT** open a public GitHub issue
2. Use [GitHub Security Advisories](https://github.com/mikeruhl/frenetik.mailerSend/security/advisories/new) to privately report the vulnerability
3. Include:
   - Description of the vulnerability
   - Steps to reproduce
   - Potential impact
   - Suggested fix (if available)

You should expect:
- Acknowledgment within 48-72 hours
- Regular updates on progress
- Credit in the security advisory (if desired)

## Security Best Practices

When using this SDK:

### API Token Security
- **Never** commit API tokens to source control
- Store tokens in environment variables or secure configuration management
- Use the `MAILERSEND_API_TOKEN` environment variable for local development
- Rotate tokens regularly
- Use scoped tokens with minimum required permissions

### Configuration
- Keep `appsettings.Development.json` and `appsettings.Local.json` out of source control
- Use User Secrets for development: `dotnet user-secrets set "MailerSend:ApiToken" "your_token"`
- In production, use secure secret management (Azure Key Vault, AWS Secrets Manager, etc.)

### Dependencies
- Keep the SDK and its dependencies up to date
- Monitor for security advisories on NuGet packages
- Run `dotnet list package --vulnerable` regularly

### HTTPS Only
- Always use HTTPS endpoints (default: `https://api.mailersend.com/v1`)
- Never disable SSL/TLS certificate validation

## Known Security Considerations

### Rate Limiting
The SDK respects MailerSend API rate limits. Implement exponential backoff in your application if you encounter rate limit errors.

### Input Validation
Always validate and sanitize user input before including it in emails to prevent:
- Email injection attacks
- XSS in HTML email content
- Header injection

### Error Handling
Avoid logging or displaying API tokens in error messages or logs.

## Third-Party Security

This SDK is a community-maintained third-party library and is not officially supported by MailerSend. For security issues related to the MailerSend API itself, please contact [MailerSend Support](https://www.mailersend.com/help).
