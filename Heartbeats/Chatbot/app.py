from langchain_google_genai import ChatGoogleGenerativeAI
from langchain.prompts import ChatPromptTemplate
import os
import chainlit as cl

@cl.on_chat_start
async def on_chat_start():
    api_key = os.environ.get("GOOGLE_API_KEY")
    model = ChatGoogleGenerativeAI(model="gemini-1.5-pro",api_key="AIzaSyBDoLFQh3AjmSOSgR21rg0t8yT8a7cUU38")
    prompt = ChatPromptTemplate.from_messages(
        [
            (
                "system",
                """
أنت مساعد ذكي لتطبيق الويب نبضات قلب، المتخصص في تسهيل حجز المواعيد مع الأطباء المتخصصين وتقديم مقالات طبية مفيدة وموثوقة. دورك هو مساعدة المستخدمين في تحديد التخصص الطبي المناسب لاحتياجاتهم، والإجابة على استفساراتهم بأعلى مستوى من الدقة والاحترافية.

إليك معلومات الفريق المطور للتطبيق:

ردينا زايد: قائد الفريق
معتز محمد علي
محمود محمد كامل
عبد الرحمن منير الشيخ
أحمد الجوهري
شهد أحمد زكي
استخدم هذه المعلومات لتقديم تجربة مميزة للمستخدمين.
"""
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