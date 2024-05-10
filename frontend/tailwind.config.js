/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
    "./public/assets/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        Inter: ['Inter', 'sans-serif']
      },
      colors: {
        'ct-black': {
          100: '#D0D5DD',
          200: '#667085',
          300: '#101828',
        },
        'ct-green': {
          100: '#C1FF72',
          200: '#7ED957',
          300: '#00BF63',
        },
        'error': '#F04438',
        'warning': '#F79009',
        'success': '#12B76A',
      },
      keyframes: {
        slideDown: {
          '0%': { transform: 'translateY(-50%)' },
          '100%': { transform: 'translate(0)' }
        },
        slideUp: {
          '0%': {
            transform: 'translateY(10px) translateX(-50%)',
            opacity: 0
          },
          '100%': {
            transform: 'translateY(0) translateX(-50%)',
            opacity: 1
          }
        }
      },
      animation: {
        slideDown: 'slideDown .5s ease-in-out',
        slideUp: 'slideUp .5s ease-in-out'
      }
    },
  },
  plugins: [],
}

