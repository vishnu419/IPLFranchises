#!/usr/bin/env node
const { execSync } = require('child_process');
try {
  execSync('dotnet dev-certs https --trust', { stdio: 'ignore' });
} catch {}
process.exit(0);
