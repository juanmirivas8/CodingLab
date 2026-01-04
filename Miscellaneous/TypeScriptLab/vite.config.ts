import { defineConfig } from 'vite'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 8080,
    open: true
  },
  build: {
    outDir: 'dist',
    sourcemap: true
  }
})

