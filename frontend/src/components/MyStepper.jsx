/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState } from "react"
import Box from "@mui/material/Box"
import Stepper from "@mui/material/Stepper"
import Step from "@mui/material/Step"
import StepLabel from "@mui/material/StepLabel"
import Typography from "@mui/material/Typography"

const steps = ["Chờ xác nhận", "Chờ lấy hàng", "Chờ giao hàng", "Đã giao hàng"]

const MyStepper = ({ stepDone }) => {
  const [activeStep, setActiveStep] = useState(stepDone)
  const [skipped, setSkipped] = useState(new Set())

  const isStepOptional = (step) => {
    return step === -1
  }

  const isStepSkipped = (step) => {
    return skipped.has(step)
  }

  return (
    <Box sx={{ width: "100%" }}>
      <Stepper activeStep={activeStep}>
        {steps.map((label, index) => {
          const stepProps = {}
          const labelProps = {}
          if (isStepOptional(index)) {
            labelProps.optional = (
              <Typography variant='caption'>Optional</Typography>
            )
          }
          if (isStepSkipped(index)) {
            stepProps.completed = false
          }
          return (
            <Step key={label} {...stepProps}>
              <StepLabel {...labelProps}>{label}</StepLabel>
            </Step>
          )
        })}
      </Stepper>
    </Box>
  )
}

export default MyStepper
