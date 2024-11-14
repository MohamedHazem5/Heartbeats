from langchain_google_genai import ChatGoogleGenerativeAI
from langchain.prompts import ChatPromptTemplate
import os
import chainlit as cl

@cl.on_chat_start
async def on_chat_start():
    api_key = os.environ.get("GOOGLE_API_KEY")
    model = ChatGoogleGenerativeAI(model="gemini-1.5-pro",api_key=api_key)
    prompt = ChatPromptTemplate.from_messages(
        [
            (
                "system",
                "You're a very knowledgeable historian who provides accurate and eloquent answers to historical questions.",
            ),

            ("human", "{history}\n\n {question}"),
        ]
    )
    chain = prompt | model
    cl.user_session.set("chain", chain)
    cl.user_session.set("history", [])

@cl.on_message
async def on_message(message: cl.Message):
    chain =  cl.user_session.get("chain")
    history =  cl.user_session.get("history")

    result = chain.invoke({'history':history,'question':message.content})
    history.extend((message.content,result.content))
    await cl.Message(content=result.content).send()